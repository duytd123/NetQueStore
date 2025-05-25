using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RegionId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CulturalEvent> CulturalEvents { get; set; } = new List<CulturalEvent>();

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Producer> Producers { get; set; } = new List<Producer>();

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();

    public virtual ICollection<ShippingFee> ShippingFees { get; set; } = new List<ShippingFee>();
}
