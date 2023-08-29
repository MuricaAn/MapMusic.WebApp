using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Event.Models
{
    public class EditEventModel
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; } 
        public int MusicTypeId { get; set; }
        public List<int> ArtistsId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[]? ProfilePhoto { get; set; }
        public IFormFile NewProfilePhoto { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }
    }
}
