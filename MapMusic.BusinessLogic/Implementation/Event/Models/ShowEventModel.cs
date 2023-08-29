using MapMusic.BusinessLogic.Implementation.Account.Models;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.Rating.Models;

namespace MapMusic.BusinessLogic.Implementation.Event.Models
{
    public class ShowEventModel
    {
        public int Id { get; set; }
        public ShowLocationModel Location { get; set; }
        public string MusicType { get; set; }

        public string Name { get; set; } = null!;
        public List<(int, string)> Artists { get; set; }
        public (int, string) Organizer { get; set; }
        public string? Description { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public List<RatingOnEventModel>? Ratings { get; set; }
        public List<NameIdModel> ArtistsForEvent { get; set; }
        public NameIdModel OrganizerEvent { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
