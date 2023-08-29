using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.Location;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.VwSearchBar;
using MapMusic.BusinessLogic.Implementation.VwSearchBar.Models;
using MapMusic.Entities.Entities;
using MapMusic.WebApp.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NuGet.Packaging.Signing;

namespace MapMusic.WebApp.Controllers
{
    public class EventController : BaseController
    {
        private readonly EventService eventService;
        private readonly LocationService locationService;
        private readonly AccountService accountService;
        private readonly VwSearBarEntitiesService vwSearBarEntitiesService;


        public EventController(ControllerDependencies dependencies, EventService eventService, LocationService locationService, AccountService accountService, VwSearBarEntitiesService vwSearBarEntitiesService) : base(dependencies)
        {
            this.eventService = eventService;
            this.locationService = locationService;
            this.accountService = accountService;
            this.vwSearBarEntitiesService = vwSearBarEntitiesService;
        }
        [HttpGet("/Event/GetMusicTypes")]
        public IActionResult GetMusicTypes()
        {
            var musicTypes = eventService.GetMusicTypes();
            return Ok(musicTypes);
        }
        [HttpGet]
        public IActionResult GetArtists()
        {
            var artists = eventService.GetArtists();
            return Ok(artists);
        }
        [HttpGet]
        [Authorize(Policy = "Organizer")]
        public IActionResult CreateEvent()
        {
           
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "Organizer")]
        public IActionResult CreateEvent(CreateEventModel model)
        {
            model.OrganizerId = CurrentUser.Id;
            eventService.CreateEvent(model);    
            return Redirect($"/Event/ShowEventsList?newEvents={true}");
        }
        public IActionResult ShowEvent(int eventId)
        {
            var showEventModel = eventService.ShowEvent(eventId);
            
            return View(showEventModel);
        }

        public IActionResult ShowEventsList( bool? newEvents, int? page, string? musicTypesString, int? maxPrice=null, DateTime? startDate=null, DateTime? endDate = null)
        {
            if (page == null)
            {
                page = 1;
            }
            List<int> musicTypes = new List<int>();
            if (musicTypesString != null && musicTypesString != "null")
            {
                musicTypes = musicTypesString.Split(',').Select(int.Parse).ToList();
            }
            List<Event> eventt;
            if (newEvents != false)
            {
                eventt = eventService.GetUpcomingEvents((int)page, musicTypes, maxPrice, startDate, endDate);
            }
            else
            {
                eventt = eventService.GetPastEvents((int)page, musicTypes, maxPrice, startDate, endDate);
            }
            var showEventModel = new List<ShowEventModel>();
            foreach (var eventtt in eventt)
            {
                showEventModel.Add(eventService.ShowEvent(eventtt.Id));
            }
            var Page = new ShowEventList
            {
                Events = showEventModel
            };
            if (newEvents != false)
            {
                Page.IsNextPage = eventService.GetUpcomingEvents((int)page + 1, musicTypes, maxPrice, startDate, endDate).Count() > 0;
            }
            else
            {
                Page.IsNextPage = eventService.GetPastEvents((int)page + 1, musicTypes, maxPrice, startDate, endDate).Count() > 0;
            }
            return View(Page);
        }

        [Authorize(Policy = "Organizer")]
        [HttpGet]
        public IActionResult Editevent(int eventId)
        {
            var @event = eventService.GetEventById(eventId);
            if (@event == null)
            {
                return Redirect($"/Event/ShowEventsList?newEvents={true}");
            }
            if (@event.OrganizerId != CurrentUser.Id)
            {
                return Redirect($"/Event/ShowEventsList?newEvents={true}");
            }
            var editevent = eventService.EditEvent(eventId);
            return View(editevent);
        }

        [Authorize(Policy = "Organizer")]
        [HttpPost]
        public IActionResult Editevent(EditEventModel model)
        {
            var @event = eventService.GetEventById(model.Id);
            if (@event == null)
            {
                return Redirect($"/Event/ShowEventsList?newEvents={true}");
            }
            if (@event.OrganizerId != CurrentUser.Id)
            {
                return Redirect($"/Event/ShowEventsList?newEvents={true}");
            }
            eventService.UpdateEvent(model);
            return Redirect("/");
        }

        [Authorize(Policy = "Organizer")]
        public IActionResult DeleteEvent(int eventId)
        {
            var @event = eventService.GetEventById(eventId);
            if (@event == null)
            {
                return Redirect($"/Event/ShowEventsList?newEvents={true}");
            }
            if (@event.OrganizerId != CurrentUser.Id)
            {
                return Redirect($"/Event/ShowEventsList?newEvents={true}");
            }
            eventService.DeleteEvent(eventId);
            return Redirect($"/Event/ShowEventsList?newEvents={true}");
        }

        [HttpGet]
        public IActionResult GetUpcomingEvents()
        {
            var upcommingEvents = eventService.GetUpcommingEvents();
            return Ok(upcommingEvents);
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult AddFavouriteEvent(int eventId, int userId)
        {
            if (userId == CurrentUser.Id)
            {
                eventService.AddFavouriteEvent(eventId, userId);
            }
            return Ok();

        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult RemoveFavouriteEvent(int eventId, int userId)
        {
            if (userId == CurrentUser.Id)
            {
                eventService.RemoveFavouriteEvent(eventId, userId);
            }
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpGet("Event/IsfavouriteEvent/{userId}/{eventId}")]
        public IActionResult IsfavouriteEvent([FromRoute]int userId, [FromRoute] int eventId)
        {
            var isfavouriteEvent = eventService.GetFavouriteEvents(userId, eventId);
            return Ok(isfavouriteEvent);
        }

        [HttpGet]
        public IActionResult GetEntitiesForSearchBar([FromQuery]string search)
        {
            var events = vwSearBarEntitiesService.GetEntitiesForSearchBar(search);
            return Json(events);
        }
    }
}
