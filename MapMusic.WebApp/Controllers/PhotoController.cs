using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.PhotoImp;
using MapMusic.BusinessLogic.Implementation.PhotoImp.Models;
using MapMusic.DataAccess;
using MapMusic.WebApp.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MapMusic.WebApp.Controllers
{
    public class PhotoController : BaseController
    {
        private readonly PhotoService photoService;
        public PhotoController(ControllerDependencies dependencies, PhotoService photoService) : base(dependencies)
        {
            this.photoService = photoService;
        }
        [HttpGet]
        public IActionResult AddPhoto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPhoto(AddPhotoModel photo)
        {
            photoService.AddPhoto(photo);
            return View();
        }
        [HttpPost]
        public IActionResult AddPhotoLocation(ShowLocationModel model)
        {
            photoService.AddPhotoLocation(model);
            return RedirectToAction("ShowLocation", "Location", new { locationId = model.Id });
            //return Redirect($"/Location/ShowLocation?locationId={model.Id}");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult DeletePhotoLocation(int photoId, int location) {
            photoService.DeletePhotoLocation(photoId);
            return RedirectToAction("ShowLocation", "Location", new { locationId = location });
        }

        [HttpGet]
        public IActionResult PhotoEvent(int eventId)
        {
            var model = photoService.PhotoEvent(eventId);
            return View(model);
        }

        [HttpPost]
        public IActionResult PhotoEvent(ShowEventPhotoModel model)
        {
            photoService.AddPhotoEvent(model);
            return RedirectToAction("PhotoEvent", "Photo", new { eventId = model.Id });
        }
        [Authorize(Policy = "Admin")]
        public IActionResult DeletePhotoEvent(int photoId, int eventid)
        {
            photoService.DeletePhotoEvent(photoId);
            return RedirectToAction("PhotoEvent", "Photo", new { eventId = eventid });
        }

        
        [HttpGet("Photo/ShowPhoto/{photoId}")]
        public IActionResult ShowPhoto([FromRoute] int photoId)
        {
            return Ok(new ImageProfileModel { Image =  photoService.GetContentById(photoId) });
        }
    }
}
