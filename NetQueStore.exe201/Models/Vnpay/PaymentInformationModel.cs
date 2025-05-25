namespace NetQueStore.exe201.Models.Vnpay
{
    public class PaymentInformationModel
    {
        public int OrderId { get; set; }
        public string OrderType { get; set; } = "other";
        public double Amount { get; set; }
        public string OrderDescription { get; set; } = "";
        public string Name { get; set; } = "";

    }
}
