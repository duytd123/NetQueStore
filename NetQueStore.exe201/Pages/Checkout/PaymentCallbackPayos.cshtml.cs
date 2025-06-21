using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Services.Payos;
using System.Text.Json;

namespace NetQueStore.exe201.Pages.Checkout
{
    public class PaymentCallbackPayosModel : PageModel
    {
        private readonly PayOSService _payOSService;
        private readonly ILogger<PaymentCallbackPayosModel> _logger;
        private readonly Exe2Context _context;

        public PaymentCallbackPayosModel(PayOSService payOSService, ILogger<PaymentCallbackPayosModel> logger, Exe2Context context)
        {
            _payOSService = payOSService;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Xử lý callback từ PayOS
                var response = _payOSService.PaymentExecute(Request.Query);
                if (response == null)
                {
                    _logger.LogWarning("PayOS response is null");
                    TempData["ErrorMessage"] = "Không thể xử lý phản hồi từ PayOS.";
                    HttpContext.Session.Remove("TempOrder");
                    return RedirectToPage("/CheckOut");
                }

                _logger.LogInformation("PayOS callback received: OrderId={OrderId}, Status={Status}, Code={Code}",
                    response.OrderId, response.Success ? "PAID" : "FAILED", response.VnPayResponseCode);

                if (!long.TryParse(response.OrderId, out long orderCode))
                {
                    _logger.LogWarning("Invalid OrderId: {OrderId}", response.OrderId);
                    TempData["ErrorMessage"] = "Mã đơn hàng không hợp lệ.";
                    HttpContext.Session.Remove("TempOrder");
                    return RedirectToPage("/CheckOut");
                }

                // Lấy thông tin đơn hàng tạm thời từ session
                var tempOrderJson = HttpContext.Session.GetString("TempOrder");
                if (string.IsNullOrEmpty(tempOrderJson))
                {
                    _logger.LogWarning("TempOrder session is empty");
                    TempData["ErrorMessage"] = "Không tìm thấy thông tin đơn hàng.";
                    HttpContext.Session.Remove("TempOrder");
                    return RedirectToPage("/CheckOut");
                }

                _logger.LogInformation("TempOrder JSON: {TempOrderJson}", tempOrderJson);

                TempOrder tempOrder;
                try
                {
                    tempOrder = JsonSerializer.Deserialize<TempOrder>(tempOrderJson);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deserializing TempOrder JSON: {TempOrderJson}", tempOrderJson);
                    TempData["ErrorMessage"] = "Lỗi khi đọc thông tin đơn hàng.";
                    HttpContext.Session.Remove("TempOrder");
                    return RedirectToPage("/CheckOut");
                }

                // Kiểm tra OrderCode từ PayOS có khớp với BankId trong tempOrder không
                if (orderCode != tempOrder.BankId)
                {
                    _logger.LogWarning("OrderCode {OrderCode} does not match BankId {BankId}", orderCode, tempOrder.BankId);
                    TempData["ErrorMessage"] = "Mã đơn hàng không khớp.";
                    HttpContext.Session.Remove("TempOrder");
                    return RedirectToPage("/CheckOut");
                }

                if (response.Success)
                {
                    // Lưu đơn hàng vào database
                    using var transaction = await _context.Database.BeginTransactionAsync();
                    var order = new NetQueStore.exe201.Models.Order
                    {
                        OrderNumber = tempOrder.OrderNumber,
                        BankId = tempOrder.BankId,
                        UserId = tempOrder.UserId,
                        SessionId = tempOrder.SessionId,
                        GuestEmail = tempOrder.GuestEmail,
                        RecipientName = tempOrder.RecipientName,
                        RecipientPhone = tempOrder.RecipientPhone,
                        RecipientEmail = tempOrder.RecipientEmail,
                        DeliveryAddress = tempOrder.DeliveryAddress,
                        DeliveryNotes = tempOrder.DeliveryNotes,
                        PaymentMethodId = tempOrder.PaymentMethodId,
                        ShippingFee = tempOrder.ShippingFee,
                        Subtotal = tempOrder.Subtotal,
                        TotalAmount = tempOrder.TotalAmount,
                        OrderDate = DateTime.Now,
                        OrderStatus = "success",
                        PaymentStatus = "paid",
                        CreatedAt = DateTime.Now,
                        TrackingNumber = tempOrder.TrackingNumber
                    };

                    _logger.LogInformation("Saving order: Subtotal={Subtotal}, ShippingFee={ShippingFee}, TotalAmount={TotalAmount}, TrackingNumber={TrackingNumber}",
                        order.Subtotal, order.ShippingFee, order.TotalAmount, order.TrackingNumber);

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Order created with ID: {OrderId}, OrderNumber: {OrderNumber}", order.Id, order.OrderNumber);

                    foreach (var kv in tempOrder.FoodQuantities)
                    {
                        var food = await _context.Foods.FindAsync(kv.Key);
                        if (food == null)
                        {
                            _logger.LogWarning("Food not found: FoodId={FoodId}", kv.Key);
                            continue;
                        }

                        decimal actualPrice;
                        try
                        {
                            actualPrice = food.SalePrice.HasValue && food.SalePrice.Value > 0 && food.SalePrice.Value < food.Price
                                ? food.SalePrice.Value
                                : food.Price;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error calculating actualPrice for FoodId={FoodId}, Name={Name}, SalePrice={SalePrice}, Price={Price}",
                                food.Id, food.Name, food.SalePrice, food.Price);
                            actualPrice = food.Price; // Fallback to Price
                        }

                        _logger.LogInformation("OrderItem: FoodId={FoodId}, Name={Name}, SalePrice={SalePrice}, Price={Price}, ActualPrice={ActualPrice}, Quantity={Quantity}",
                            food.Id, food.Name, food.SalePrice, food.Price, actualPrice, kv.Value);

                        var orderItem = new OrderItem
                        {
                            OrderId = order.Id,
                            FoodId = food.Id,
                            ProductName = food.Name,
                            Quantity = kv.Value,
                            UnitPrice = actualPrice,
                            Subtotal = actualPrice * kv.Value,
                            CreatedAt = DateTime.Now
                        };

                        _context.OrderItems.Add(orderItem);
                    }

                    var paymentRecord = new VnpayPayment
                    {
                        TransactionId = response.TransactionId ?? Guid.NewGuid().ToString(),
                        OrderId = order.Id,
                        OrderDescription = response.OrderDescription ?? "Thanh toán PayOS",
                        PaymentMethod = response.PaymentMethod,
                        PaymentId = response.PaymentId,
                        VnpayResponseCode = response.VnPayResponseCode,
                        DateCreated = DateTime.Now
                    };

                    _context.VnpayPayments.Add(paymentRecord);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Xóa cookie và session
                    Response.Cookies.Delete("shopping_cart");
                    HttpContext.Session.Remove("TempOrder");

                    TempData["SuccessMessage"] = "Đặt hàng và thanh toán qua PayOS thành công!";
                    return RedirectToPage("/OrderComplete", new { id = order.Id });
                }
                else
                {
                    // Xóa session nếu thanh toán thất bại
                    HttpContext.Session.Remove("TempOrder");
                    TempData["ErrorMessage"] = $"Thanh toán không thành công hoặc đã bị hủy. Mã lỗi: {response.VnPayResponseCode}";
                    return RedirectToPage("/CheckOut");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý callback từ PayOS.");
                HttpContext.Session.Remove("TempOrder");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình xử lý thanh toán.";
                return RedirectToPage("/CheckOut");
            }
        }
    }
}