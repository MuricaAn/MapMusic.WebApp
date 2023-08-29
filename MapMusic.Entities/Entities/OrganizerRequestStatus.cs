using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class OrganizerRequestStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<OrganizerRequest> OrganizerRequests { get; set; } = new List<OrganizerRequest>();
}
