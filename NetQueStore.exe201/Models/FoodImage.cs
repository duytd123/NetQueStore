using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class FoodImage
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public string Filename { get; set; } = null!;

    public bool IsPrimary { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Food Food { get; set; } = null!;
}
