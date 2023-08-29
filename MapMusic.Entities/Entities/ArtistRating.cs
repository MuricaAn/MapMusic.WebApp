using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class ArtistRating
{
    public int Id { get; set; }

    public int RatingId { get; set; }

    public int ArtistId { get; set; }

    public int Rating { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Rating RatingNavigation { get; set; } = null!;
}
