using MapMusic.BusinessLogic.Implementation.Account.Models;
using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.WebApp.Code;
using Microsoft.AspNetCore.Mvc;
using MapMusic.BusinessLogic.Implementation.Location;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.Event;
using Microsoft.AspNetCore.Authorization;

namespace MapMusic.WebApp.Controllers
{
    public class LocationController : BaseController
    {
        private readonly LocationService locationService;
        public LocationController(ControllerDependencies dependencies, LocationService locationService) : base(dependencies)
        {
            this.locationService = locationService;
        }

        [Authorize(Policy = "Organizer")]
        [HttpGet]
        public IActionResult CreateLocation()
        {
            return View();
        }

        [Authorize(Policy = "Organizer")]
        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationModel model)
        {
            if (locationService.IsAnyLocationWithThisName(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "This name already exists");
                return View(model);
            }
            if (locationService.IsAnyLocationWithThisAddress(model.Address))
            {
                ModelState.AddModelError(nameof(model.Address), "This address already exists");
                return View(model);
            }
            locationService.AddLocation(model);
            return Redirect("/");
        }

        public IActionResult ShowLocation(int locationId)
        {
            var showLocationModel = locationService.ShowLocation(locationId);
            return View(showLocationModel);
        }

        [HttpGet("/Location/GetLocation")]
        public IActionResult GetLocation()
        {
            var locations = locationService.GetLocation();
            return Ok(locations);
        }

        [HttpGet]
        public IActionResult GetLocationUpcomingEvents(int page, int locationId)
        {
            var events = locationService.GetLocationUpcomingEvents(page, locationId);
            foreach (var @event in events)
            {
                foreach(var artist in @event.Artists)
                {
                    @event.ArtistsForEvent.Add(
                        new NameIdModel
                        {
                            Id = artist.Item1,
                            Name = artist.Item2
                        }
                        );
                }
                @event.OrganizerEvent = new NameIdModel
                {
                    Id = @event.Organizer.Item1,
                    Name = @event.Organizer.Item2
                };
            }
            return Ok(events);
        }

        [HttpGet]
        public IActionResult GetPastEvents(int page, int locationId)
        {
            var events = locationService.GetLocationPastEvents(page, locationId);
            return Ok(events);
        }
    }
}
