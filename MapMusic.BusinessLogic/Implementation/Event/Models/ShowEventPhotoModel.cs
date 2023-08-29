using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Event.Models
{
    public class ShowEventPhotoModel
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string? PhotoDescription { get; set; }
        public List<(string, byte[], int)> Photos { get; set; }
    }
}
