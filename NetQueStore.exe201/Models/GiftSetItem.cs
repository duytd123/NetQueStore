using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class GiftSetItem
{
    public int GiftSetId { get; set; }

    public int FoodId { get; set; }

    public int Quantity { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual GiftSet GiftSet { get; set; } = null!;
}
