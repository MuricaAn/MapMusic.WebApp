using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int CredentialId { get; set; }

    public int PhotoId { get; set; }

    public int RoleId { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Credential Credential { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
