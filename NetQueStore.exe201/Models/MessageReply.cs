﻿using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class MessageReply
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public int UserId { get; set; }

    public string Reply { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
