using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class CulturalEvent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? ProvinceId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? ImageFilename { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FoodEvent> FoodEvents { get; set; } = new List<FoodEvent>();

    public virtual Province? Province { get; set; }
}
