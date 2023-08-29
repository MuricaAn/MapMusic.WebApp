using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class PhotoEvent
{
    public int EventId { get; set; }

    public int PhotoId { get; set; }

    public string? Description { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
