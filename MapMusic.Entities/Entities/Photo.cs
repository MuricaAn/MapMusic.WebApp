using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Photo
{
    public int Id { get; set; }

    public byte[]? Content { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();

    public virtual ICollection<PhotoEvent> PhotoEvents { get; set; } = new List<PhotoEvent>();

    public virtual ICollection<PhotoLocation> PhotoLocations { get; set; } = new List<PhotoLocation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
