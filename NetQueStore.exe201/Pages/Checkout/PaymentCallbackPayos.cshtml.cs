using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Services.Payos;

namespace NetQueStore.exe201.Pages.Checkout
{
    public class PaymentCallbackPayosModel : PageModel
    {
        private readonly PayOSService _payOSService;
        private readonly ILogger<PaymentCallbackPayosModel> _logger;
        private readonly Exe2Context _context;

        public PaymentCallbackPayosModel(PayOSService payOSService,ILogger<PaymentCallbackPayosModel> logger, Exe2Context context)
        {
            _payOSService = payOSService;
            _logger = logger;
            _context = context;
        }

        public IActionResult OnGet()
        {
            try
            {
                var response = _payOSService.PaymentExecute(Request.Query);
                if (response == null)
                {
                    TempData["ErrorMessage"] = "Không thể xử lý phản hồi từ PayOS.";
                    return RedirectToPage("/CheckOut");
                }

                _logger.LogInformation("PayOS callback received: OrderId={OrderId}, Status={Status}, Code={Code}",
                    response.OrderId, response.Success ? "PAID" : "FAILED", response.VnPayResponseCode);

                if (!long.TryParse(response.OrderId, out long orderCode))
                {
                    TempData["ErrorMessage"] = "Mã đơn hàng không hợp lệ.";
                    return RedirectToPage("/CheckOut");
                }

                var order = _context.Orders.FirstOrDefault(o => o.BankId == orderCode);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn hàng.";
                    return RedirectToPage("/CheckOut");
                }

                if (response.Success)
                {
                    order.PaymentStatus = "paid";
                    order.OrderStatus = "success";
                    order.OrderDate = DateTime.Now;

                    var paymentRecord = new VnpayPayment
                    {
                        TransactionId = response.TransactionId ?? Guid.NewGuid().ToString(),
                        OrderId = order.Id,
                        OrderDescription = response.OrderDescription ?? "Thanh toan PayOS",
                        PaymentMethod = response.PaymentMethod,
                        PaymentId = response.PaymentId,
                        VnpayResponseCode = response.VnPayResponseCode,
                        DateCreated = DateTime.Now
                    };

                    _context.VnpayPayments.Add(paymentRecord);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Đặt hàng và thanh toán qua PayOS thành công!";
                    return RedirectToPage("/OrderComplete", new { id = order.Id });
                }
                else
                {
                    order.PaymentStatus = "failed";
                    order.OrderStatus = "cancelled";
                    _context.SaveChanges();

                    TempData["ErrorMessage"] = $"Thanh toán không thành công hoặc đã bị hủy. Mã lỗi: {response.VnPayResponseCode}";
                    return RedirectToPage("/CheckOut");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý callback từ PayOS.");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình xử lý thanh toán.";
                return RedirectToPage("/CheckOut");
            }
        }

    }
}

