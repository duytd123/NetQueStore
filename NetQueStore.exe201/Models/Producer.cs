using System;
using System.Collections.Generic;

namespace NetQueStore.exe201.Models;

public partial class Producer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public int ProvinceId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public int? EstablishedYear { get; set; }

    public string? ImageFilename { get; set; }

    public bool Featured { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FoodProducer> FoodProducers { get; set; } = new List<FoodProducer>();

    public virtual Province Province { get; set; } = null!;
}
