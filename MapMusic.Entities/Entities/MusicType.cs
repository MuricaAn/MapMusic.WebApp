using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class MusicType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
