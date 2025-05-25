using NetQueStore.exe201.Models.PayOs;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Net.payOS.Types;
using Net.payOS;
using NetQueStore.exe201.Models.Vnpay;
using Microsoft.Extensions.Logging;


namespace NetQueStore.exe201.Services.Payos
{

public class PayOSService
    {
        private readonly PayOS _payOS;
        private readonly ILogger<PayOSService> _logger;

        public PayOSService(PayOS payOS, IConfiguration configuration, ILogger<PayOSService> logger)
        {
            _payOS = payOS;
            _logger = logger;

        }

        public async Task<CreatePaymentResult?> CreatePaymentAsync(PayOSPaymentRequest payReq)
        {
            long orderCode;
            if (!long.TryParse(payReq.OrderCode, out orderCode))
            {
                orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            List<ItemData> items = new()
    {
        new ItemData(payReq.Description, 1, payReq.Amount)
    };

            PaymentData paymentData = new(
                orderCode,
                payReq.Amount,
                payReq.Description,
                items,
                payReq.CancelUrl,
                payReq.ReturnUrl
            );

            try
            {
                return await _payOS.createPaymentLink(paymentData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PayOS Error: {ex.Message}");
                return null;
            }
        }

        public PaymentResponseModel PaymentExecute(IQueryCollection query)
        {
            try
            {
                _logger.LogInformation("Processing PayOS callback with query: {Query}", query);

                var orderId = query["orderCode"].ToString(); 
                var status = query["status"].ToString();    
                var code = query["code"].ToString();        
                var cancel = query["cancel"].ToString();     
                var token = query["id"].ToString();          

                var isSuccess = code == "00" && status.ToUpper() == "PAID" && cancel.ToLower() != "true";

                var response = new PaymentResponseModel
                {
                    Success = isSuccess,
                    PaymentMethod = "PayOS",
                    OrderDescription = $"PayOS#{orderId}",
                    OrderId = orderId,
                    PaymentId = token,
                    TransactionId = token,
                    Token = token,
                    VnPayResponseCode = code
                };

                _logger.LogInformation("PayOS PaymentExecute response: {@Response}", response);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý callback từ PayOS.");
                return new PaymentResponseModel
                {
                    Success = false
                };
            }

        }

    }
}