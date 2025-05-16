using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class FoodEvent
{
    public int FoodId { get; set; }

    public int EventId { get; set; }

    public string? Description { get; set; }

    public virtual CulturalEvent Event { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
