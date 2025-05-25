using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool IsRead { get; set; }

    public string? Link { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
