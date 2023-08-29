using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class OrganizerArtistInvitation
{
    public int Id { get; set; }

    public int OrganizerId { get; set; }

    public int ArtistId { get; set; }

    public int EventId { get; set; }

    public int OrganizerArtistInvitationStatusId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Organizer Organizer { get; set; } = null!;

    public virtual OrganizerArtistInvitationStatus OrganizerArtistInvitationStatus { get; set; } = null!;
}
