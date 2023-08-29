using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Event
{
    public int Id { get; set; }

    public int OrganizerId { get; set; }

    public int LocationId { get; set; }

    public int MusicTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public byte[]? ProfilePhoto { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual MusicType MusicType { get; set; } = null!;

    public virtual Organizer Organizer { get; set; } = null!;

    public virtual ICollection<OrganizerArtistInvitation> OrganizerArtistInvitations { get; set; } = new List<OrganizerArtistInvitation>();

    public virtual ICollection<PhotoEvent> PhotoEvents { get; set; } = new List<PhotoEvent>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
