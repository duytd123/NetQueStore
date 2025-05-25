using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Setting
{
    public int Id { get; set; }

    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string SettingGroup { get; set; } = null!;

    public bool IsPublic { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
