using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.VwSearchBar.Models
{
    public class VwSearchBarEntitiesModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string EntityType { get; set; }
        public byte[]? ProfilePhoto { get; set; }
    }
}
