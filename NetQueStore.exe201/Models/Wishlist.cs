using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Wishlist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FoodId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
