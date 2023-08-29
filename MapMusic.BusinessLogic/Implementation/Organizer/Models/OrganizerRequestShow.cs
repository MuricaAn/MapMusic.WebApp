using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Organizer.Models
{
    public class OrganizerRequestShow
    {
        public int Id { get; set; }

        public int OrganizerRequestStatusId { get; set; }

        public string FullName { get; set; } = null!;

        public string? Description { get; set; }

        public string Email { get; set; } = null!;
    }
}
