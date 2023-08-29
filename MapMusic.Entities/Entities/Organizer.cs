using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Organizer
{
    public int Id { get; set; }

    public int CredentialId { get; set; }

    public int PhotoId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Credential Credential { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<OrganizerArtistInvitation> OrganizerArtistInvitations { get; set; } = new List<OrganizerArtistInvitation>();

    public virtual Photo Photo { get; set; } = null!;
}
