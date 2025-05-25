using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class BankAccount
{
    public int Id { get; set; }

    public int BankId { get; set; }

    public string AccountName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string? Branch { get; set; }

    public bool IsPrimary { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Bank Bank { get; set; } = null!;
}
