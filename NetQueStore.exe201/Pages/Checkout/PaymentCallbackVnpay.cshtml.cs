using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Services.Vnpay;

namespace NetQueStore.exe201.Pages.Checkout
{
    public class PaymentCallbackVnpayModel : PageModel
    {
        private readonly IVnPayService _vnPayService;
        private readonly ILogger<PaymentCallbackVnpayModel> _logger;
        private readonly Exe2Context _context;


        public PaymentCallbackVnpayModel(IVnPayService vnPayService, ILogger<PaymentCallbackVnpayModel> logger, Exe2Context context)
        {
            _vnPayService = vnPayService;
            _logger = logger;
            _context = context;

        }

        public IActionResult OnGet()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                if (response == null)
                {
                    TempData["ErrorMessage"] = "Không thể xử lý kết quả thanh toán.";
                    return RedirectToPage("/CheckOut");
                }

                // Parse OrderId từ string sang int
                if (!int.TryParse(response.OrderId, out int orderId))
                {
                    TempData["ErrorMessage"] = "OrderId không hợp lệ.";
                    return RedirectToPage("/CheckOut");
                }

                var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn hàng.";
                    return RedirectToPage("/CheckOut");
                }

                // Phần xử lý thanh toán
                if (response.VnPayResponseCode == "00")
                {
                    order.PaymentStatus = "paid";
                    order.OrderStatus = "success";
                    order.OrderDate = DateTime.Now;

                    // Lưu thông tin thanh toán vào bảng vnpay_payments
                    var paymentRecord = new VnpayPayment
                    {
                        TransactionId = response.TransactionId,
                        OrderId = order.Id,
                        OrderDescription = response.OrderDescription,
                        PaymentMethod = response.PaymentMethod,
                        PaymentId = response.PaymentId,
                        VnpayResponseCode = response.VnPayResponseCode,
                        DateCreated = DateTime.Now
                    };

                    _context.VnpayPayments.Add(paymentRecord);

                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Đặt hàng và thanh toán thành công!";
                    return RedirectToPage("/OrderComplete", new { id = orderId });
                }

            
                else
                {
                    order.PaymentStatus = "failed";
                    order.OrderStatus = "cancelled";
                    _context.SaveChanges();

                    TempData["ErrorMessage"] = $"Thanh toán không thành công. Mã lỗi: {response.VnPayResponseCode}";
                    return RedirectToPage("/CheckOut");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý callback VNPAY");
                return Content($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
