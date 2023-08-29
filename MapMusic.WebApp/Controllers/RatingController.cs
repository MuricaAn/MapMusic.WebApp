using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Rating;
using MapMusic.BusinessLogic.Implementation.Rating.Models;
using MapMusic.WebApp.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MapMusic.WebApp.Controllers
{
    public class RatingController : BaseController
    {
        private readonly RatingService ratingService;
        private readonly EventService eventService;
        public RatingController(ControllerDependencies dependencies, RatingService ratingService, EventService eventService) : base(dependencies)
        {
            this.ratingService = ratingService;
            this.eventService = eventService;
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult RateEvent(int eventId)
        {
            var giveRatingModel = ratingService.GetGiveRatingModel(eventId);
            return View(giveRatingModel);
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult RateEvent(GiveRatingModel model)
        {
            if (model.RatingLocation == 0)
            {
                ModelState.AddModelError(nameof(model.RatingLocation), "Rating mandatory");
            }
            if (model.RatingOrganization == 0)
            {
                ModelState.AddModelError(nameof(model.RatingOrganization), "Rating mandatory");
            }
            if (model.RatingLocation == 0 || model.RatingOrganization == 0)
            {
                var giveRatingModel = ratingService.GetGiveRatingModel(model.EventId);
                model.EventName = giveRatingModel.EventName;
                model.LocationName = giveRatingModel.LocationName;
                model.OrganizerName = giveRatingModel.OrganizerName;
                model.PresentArtists = giveRatingModel.PresentArtists;
                return View(model);
            }
            if (model.IsRated == true)
            {
                ratingService.UpdateRating(model);
            }
            else
            {
                ratingService.CreateRating(model);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
