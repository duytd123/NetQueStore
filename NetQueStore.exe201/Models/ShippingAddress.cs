using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class ShippingAddress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string RecipientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int ProvinceId { get; set; }

    public int DistrictId { get; set; }

    public int WardId { get; set; }

    public string AddressDetail { get; set; } = null!;

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Ward Ward { get; set; } = null!;
}
