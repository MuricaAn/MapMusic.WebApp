using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class PhotoLocation
{
    public int LocationId { get; set; }

    public int PhotoId { get; set; }

    public string? Description { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
