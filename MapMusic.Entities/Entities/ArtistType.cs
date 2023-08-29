using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class ArtistType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();
}
