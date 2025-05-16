using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class FoodProducer
{
    public int FoodId { get; set; }

    public int ProducerId { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual Producer Producer { get; set; } = null!;
}
