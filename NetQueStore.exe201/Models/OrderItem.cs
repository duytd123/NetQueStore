using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public int? FoodId { get; set; }
    public string? ProductName { get; set; }
    public int? Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Subtotal { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual Food? Food { get; set; }
    public virtual Order? Order { get; set; }
}
