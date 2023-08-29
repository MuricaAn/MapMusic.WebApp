using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Testview
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? ProfilePhoto { get; set; }

    public string EntityType { get; set; } = null!;
}
