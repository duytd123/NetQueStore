using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class ActivityLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string ActivityType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
