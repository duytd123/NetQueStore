using System.ComponentModel.DataAnnotations;

namespace NetQueStore.exe201.Models
{
    public class VnpayModel
    {
        [Key]
        public int Id { get; set; }
        public string OrderDescription { get; set; }

        public  string TransactionId { get; set; }

        public int OrderId { get; set; }

        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public string VnPayResponseCode { get; set; }

        public string DateCreated { get; set; }

    }
}
