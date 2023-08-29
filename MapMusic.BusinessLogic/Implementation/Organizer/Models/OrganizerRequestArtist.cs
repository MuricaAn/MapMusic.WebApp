using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Organizer.Models
{
    public class OrganizerRequestArtist
    {
        public int Id { get; set; }

        public int OrganizerArtistInvitationStatusId { get; set; }

        public int ArtistId { get; set; }
        public int EventId { get; set; }
        public int OrganizerId { get; set; }
        public string OrganizerFullName { get; set; }
        public string EventName { get; set; }
        public DateTimeOffset StartDate { get; set; }
    }
}
