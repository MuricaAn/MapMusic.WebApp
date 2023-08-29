using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Location.Models
{
    public class CreateLocationModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Address { get; set; } 
        public string Description { get; set; }
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
