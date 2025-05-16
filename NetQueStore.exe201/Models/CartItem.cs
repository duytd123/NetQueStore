using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int FoodId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
