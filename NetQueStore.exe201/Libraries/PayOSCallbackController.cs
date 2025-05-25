using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetQueStore.exe201.Models;
using NetQueStore.exe201.Services.Payos;
using Newtonsoft.Json;

namespace NetQueStore.exe201.Libraries
{
    [ApiController]
    [Route("api/payos/callback")]
    public class PayOSCallbackController : Controller
    {
       
        private readonly ILogger<PayOSCallbackController> _logger;
        private readonly Exe2Context _context;

        public PayOSCallbackController( ILogger<PayOSCallbackController> logger, Exe2Context context)
        {
          
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Callback()
        {
            using var reader = new StreamReader(Request.Body);
            var rawBody = await reader.ReadToEndAsync();
            var signature = Request.Headers["x-signature"].ToString();

            //if (!_payOSService.VerifyCallbackSignature(rawBody, signature))
            //{
            //    _logger.LogWarning("PayOS callback signature invalid.");
            //    return Unauthorized();
            //}

            var data = JsonConvert.DeserializeObject<dynamic>(rawBody);
            string orderCode = data.orderCode;
            string status = data.status;

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderCode);
            if (order == null)
            {
                _logger.LogWarning("Order not found: {0}", orderCode);
                return NotFound();
            }

            // Cập nhật trạng thái đơn
            order.PaymentStatus = status == "PAID" ? "paid" : "failed";
            order.UpdatedAt = DateTime.Now;

            // Tạo bản ghi thanh toán chung dùng bảng VnpayPayments
            var paymentRecord = new VnpayPayment
            {
                TransactionId = data.transactionId != null ? (string)data.transactionId : null,
                OrderId = order.Id,
                OrderDescription = data.orderDescription != null ? (string)data.orderDescription : null,
                PaymentMethod = "PayOS", 
                PaymentId = data.paymentId != null ? (string)data.paymentId : null,
                VnpayResponseCode = status == "PAID" ? "00" : "Failed", 
                DateCreated = DateTime.Now
            };

            _context.VnpayPayments.Add(paymentRecord);
            await _context.SaveChangesAsync();

            // Nếu thanh toán thành công, redirect tới trang OrderComplete
            if (status == "PAID")
            {
                return Redirect($"/OrderComplete/{order.Id}");
            }
            else
            {
                return BadRequest("Payment failed");
            }
        }
    }
}
