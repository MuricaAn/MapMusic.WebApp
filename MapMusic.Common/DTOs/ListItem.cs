using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.Common.DTOs
{
    public class ListItem<TText, TValue>
    {
        public TText Text { get; set; }
        public TValue Value { get; set; }
    }
}
