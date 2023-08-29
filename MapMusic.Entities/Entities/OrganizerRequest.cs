using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class OrganizerRequest
{
    public int Id { get; set; }

    public int OrganizerRequestStatusId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Description { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual OrganizerRequestStatus OrganizerRequestStatus { get; set; } = null!;
}
