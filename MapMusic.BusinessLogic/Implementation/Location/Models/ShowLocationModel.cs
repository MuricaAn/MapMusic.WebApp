using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.Entities.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Location.Models
{
    public class ShowLocationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public float Rating { get; set; }
        public IFormFile Photo { get; set; }
        public string? PhotoDescription { get; set; }
        public List<(string, byte[], int)> Photos { get; set; }
        public List<ShowEventModel> UpcomingEvents { get; set; }
        public List<ShowEventModel> PastEvents { get; set; }
    }
}
