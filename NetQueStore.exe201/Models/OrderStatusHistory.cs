using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class OrderStatusHistory
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public string? Comment { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;
}
