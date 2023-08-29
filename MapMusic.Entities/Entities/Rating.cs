using System;
using System.Collections.Generic;

namespace MapMusic.Entities.Entities;

public partial class Rating
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int EventId { get; set; }

    public int RatingLocation { get; set; }

    public int RatingOrganization { get; set; }

    public string? Comment { get; set; }

    public virtual ICollection<ArtistRating> ArtistRatings { get; set; } = new List<ArtistRating>();

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
