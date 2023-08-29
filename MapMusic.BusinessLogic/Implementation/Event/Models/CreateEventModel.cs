using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Event.Models
{
    public class CreateEventModel
    {
        public int OrganizerId { get; set; }

        public int? LocationId { get; set; }

        public int MusicTypeId { get; set; }
        public List<int>? ArtistsId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile? ProfilePhoto { get; set; }
        public decimal Price { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }
    }
}
