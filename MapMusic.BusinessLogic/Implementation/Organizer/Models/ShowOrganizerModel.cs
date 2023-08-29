using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Organizer.Models
{
    public class ShowOrganizerModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public Photo ProfilePhoto { get; set; }
        public float Rating { get; set; }
        public List<ShowEventModel> UpcomingEvents { get; set; }
        public List<ShowEventModel> PastEvents { get; set; }
    }
}
