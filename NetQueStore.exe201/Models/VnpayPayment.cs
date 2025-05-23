using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class VnpayPayment
{
    public int Id { get; set; }

    public string TransactionId { get; set; } = null!;

    public int OrderId { get; set; }

    public string? OrderDescription { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentId { get; set; }

    public string? VnpayResponseCode { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Order Order { get; set; } = null!;
}
