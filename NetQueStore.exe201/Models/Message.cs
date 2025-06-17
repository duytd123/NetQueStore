using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetQueStore.exe201.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Subject { get; set; } = null!;
    [Column("message")]
    public string Message1 { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool IsResolved { get; set; }

    public int? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual ICollection<MessageReply> MessageReplies { get; set; } = new List<MessageReply>();
}
