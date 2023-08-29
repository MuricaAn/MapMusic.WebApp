using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Rating.Models
{
    public class RatingOnEventModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
        public float OverallRating { get; set; }
    }
}
