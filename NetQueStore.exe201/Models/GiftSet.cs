using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class GiftSet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? SalePrice { get; set; }

    public string? ImageFilename { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsActive { get; set; }

    public string? Occasion { get; set; }

    public string? PackagingType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<GiftSetItem> GiftSetItems { get; set; } = new List<GiftSetItem>();
}
