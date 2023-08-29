using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Credential
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
