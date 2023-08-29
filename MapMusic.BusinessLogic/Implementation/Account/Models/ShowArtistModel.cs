using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Models
{
    public class ShowArtistModel
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public string Description { get; set; }
        public Photo ProfilePhoto { get; set; }
        public float Rating { get; set; }
        public List<ShowEventModel> UpcomingEvents { get; set; }
        public List<ShowEventModel> PastEvents { get; set; }
    }
}
