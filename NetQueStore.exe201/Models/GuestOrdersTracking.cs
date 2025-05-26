using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class GuestOrdersTracking
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string TrackingCode { get; set; } = null!;

    public string GuestEmail { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
