using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class NewsletterSubscription
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime SubscribedAt { get; set; }

    public DateTime? UnsubscribedAt { get; set; }
}
