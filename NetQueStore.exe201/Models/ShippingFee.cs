using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class ShippingFee
{
    public int Id { get; set; }

    public int ProvinceId { get; set; }

    public decimal Fee { get; set; }

    public decimal? MinOrderValue { get; set; }

    public decimal? FreeShippingMin { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Province Province { get; set; } = null!;
}
