using MapMusic.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Rating.Models
{
    public class GiveRatingModel
    {
        public string EventName { get; set; }
        public string LocationName { get; set; }
        public string OrganizerName { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int RatingLocation { get; set; }
        public int RatingOrganization { get; set; }
        public string? Comment { get; set; }
        public List<Artist> PresentArtists { get; set; }
        public List<int> RatingsForArtists { get; set; }
        public List<int> ArtistsId { get; set; }
        public bool IsRated { get; set; }
    }
}
