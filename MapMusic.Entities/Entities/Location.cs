using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<PhotoLocation> PhotoLocations { get; set; } = new List<PhotoLocation>();
}
