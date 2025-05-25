namespace NetQueStore.exe201.Models.PayOs
{
    public class PayOSPaymentRequest
    {
        public int Amount { get; set; }
        public string Description { get; set; } = "";
        public string ReturnUrl { get; set; } = "";
        public string CancelUrl { get; set; } = "";
        public string OrderCode { get; set; } = "";
    }

}
