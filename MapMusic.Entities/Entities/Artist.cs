using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Artist
{
    public int Id { get; set; }

    public int CredentialId { get; set; }

    public int PhotoId { get; set; }

    public int ArtistTypeId { get; set; }

    public string StageName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ArtistRating> ArtistRatings { get; set; } = new List<ArtistRating>();

    public virtual ArtistType ArtistType { get; set; } = null!;

    public virtual Credential Credential { get; set; } = null!;

    public virtual ICollection<OrganizerArtistInvitation> OrganizerArtistInvitations { get; set; } = new List<OrganizerArtistInvitation>();

    public virtual Photo Photo { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
