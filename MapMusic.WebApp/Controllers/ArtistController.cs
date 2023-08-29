using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Organizer;
using MapMusic.BusinessLogic.Implementation.Organizer.Models;
using MapMusic.WebApp.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MapMusic.WebApp.Controllers
{
    public class ArtistController : BaseController
    {
        private readonly OrganizerService organizerService;
        private readonly AccountService accountService;
        private readonly EventService eventService;



        public ArtistController(ControllerDependencies dependencies, OrganizerService organizerService, AccountService accountService, EventService eventService) : base(dependencies)
        {
            this.organizerService = organizerService;
            this.accountService = accountService;
            this.eventService = eventService;


        }
        [Authorize(Policy = "Artist")]
        [HttpGet]
        public IActionResult ShowArtistInvitations()
        {
            var eventInvitations = organizerService.GetEventInvitations(CurrentUser.Id);
            var listOfEventInvitations = new List<OrganizerRequestArtist>();
            foreach (var eventInvitation in eventInvitations)
            {
                eventInvitation.Event = eventService.GetEventById(eventInvitation.EventId);
                eventInvitation.Organizer = accountService.GetOrganizer(eventInvitation.OrganizerId);
                listOfEventInvitations.Add(new OrganizerRequestArtist
                {
                    Id = eventInvitation.Id,
                    ArtistId = eventInvitation.ArtistId,
                    EventId = eventInvitation.EventId,
                    OrganizerId = eventInvitation.OrganizerId,
                    OrganizerFullName = eventInvitation.Organizer.FullName,
                    EventName = eventInvitation.Event.Name,
                    OrganizerArtistInvitationStatusId = eventInvitation.OrganizerArtistInvitationStatusId,
                    StartDate = eventInvitation.Event.StartDate
                });
            }
            return Ok(listOfEventInvitations);
        }

        [Authorize(Policy = "Artist")]
        [HttpPost]
        public IActionResult AcceptOrganizerArtistInvitation(int organizerRequestId)
        {
            organizerService.AcceptOrganizerArtistInvitation(organizerRequestId);
            return Redirect("/Artist/ShowEventInvitations");
        }
        [Authorize(Policy = "Artist")]
        [HttpPost]
        public IActionResult RejectOrganizerArtistInvitation(int organizerRequestId)
        {
            organizerService.RejectOrganizerArtistInvitation(organizerRequestId);
            return Redirect("/Artist/ShowEventInvitations");
        }

        [Authorize(Policy = "Artist")]
        [HttpGet]
        public IActionResult GetOrganizerArtistInvitations()
        {
            return Ok(organizerService.GetEventInvitations(CurrentUser.Id));
        }

        [Authorize(Policy = "Artist")]
        public IActionResult ShowEventInvitations()
        {
            return View(CurrentUser.Id);
        }

        public IActionResult ShowArtist(int artistId)
        {
            var showArtistModel = accountService.ShowArtist(artistId);
            return View(showArtistModel);
        }
    }
}
