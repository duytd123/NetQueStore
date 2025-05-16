using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Food
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public int RegionId { get; set; }

    public int ProvinceId { get; set; }

    public decimal Price { get; set; }

    public decimal? SalePrice { get; set; }

    public int StockQuantity { get; set; }

    public string Unit { get; set; } = null!;

    public decimal? Weight { get; set; }

    public string? ShelfLife { get; set; }

    public string? StorageInstructions { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsSpecial { get; set; }

    public bool IsVegetarian { get; set; }

    public bool IsActive { get; set; }

    public string? Ingredients { get; set; }

    public string? PreparationMethod { get; set; }

    public string? ServingSuggestion { get; set; }

    public string? CulturalSignificance { get; set; }

    public string? Allergens { get; set; }

    public string? Certification { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<FoodEvent> FoodEvents { get; set; } = new List<FoodEvent>();

    public virtual ICollection<FoodImage> FoodImages { get; set; } = new List<FoodImage>();

    public virtual ICollection<FoodProducer> FoodProducers { get; set; } = new List<FoodProducer>();

    public virtual ICollection<GiftSetItem> GiftSetItems { get; set; } = new List<GiftSetItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Province Province { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
