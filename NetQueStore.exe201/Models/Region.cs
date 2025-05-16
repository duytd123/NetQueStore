using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageFilename { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}
