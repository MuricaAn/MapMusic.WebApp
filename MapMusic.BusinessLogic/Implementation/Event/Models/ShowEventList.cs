using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Event.Models
{
    public class ShowEventList
    {
        public bool IsNextPage { get; set; }
        public List<ShowEventModel> Events { get; set; }
    }
}
