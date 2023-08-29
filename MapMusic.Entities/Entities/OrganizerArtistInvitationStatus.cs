using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class OrganizerArtistInvitationStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OrganizerArtistInvitation> OrganizerArtistInvitations { get; set; } = new List<OrganizerArtistInvitation>();
}
