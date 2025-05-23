using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetQueStore.exe201.Models.Vnpay;
using NetQueStore.exe201.Services.Vnpay;
using System;

namespace NetQueStore.exe201.Pages
{
    public class VnpayTestModel : PageModel
    {
        private readonly IVnPayService _vnPayService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<VnpayTestModel> _logger;

        public VnpayTestModel(IVnPayService vnPayService, IHttpContextAccessor httpContextAccessor, ILogger<VnpayTestModel> logger)
        {
            _vnPayService = vnPayService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            try
            {
                var paymentInfo = new PaymentInformationModel
                {
                    Amount = 415000,
                    Name = "Khách",
                    OrderDescription = "Đơn hàng test thanh toán",
                    OrderType = "other", 
                    OrderId = 15
                };

                var url = _vnPayService.CreatePaymentUrl(paymentInfo, HttpContext);
                _logger.LogInformation("Redirecting to VNPay: {Url}", url);

                // ✅ Dùng RedirectResult thủ công để tránh lỗi app crash
                return Task.Run(() => Redirect(url)).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo URL thanh toán");
                return Content("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
