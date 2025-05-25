using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class OutOfStockNotification
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public string Email { get; set; } = null!;

    public bool IsNotified { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? NotifiedAt { get; set; }

    public virtual Food Food { get; set; } = null!;
}
