using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.PhotoImp.Models;
using MapMusic.BusinessLogic.Implementation.PhotoImp.Validations;
using MapMusic.Common.Extensions;
using MapMusic.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.PhotoImp
{
    public class PhotoService : BaseService
    {
        private readonly AddPhotoValidator addPhotoValidator;
        public PhotoService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            addPhotoValidator = new AddPhotoValidator();
        }

        public void AddPhoto(AddPhotoModel model)
        {
            var photo = new Photo
            {
                CreatedOn = DateTime.Now
            };
            using (var ms = new MemoryStream())
            {
                model.Content.CopyTo(ms);
                photo.Content = ms.ToArray();
            }
            UnitOfWork.Photos.Insert(photo);
            UnitOfWork.SaveChanges();
        }

        public void AddPhotoLocation(ShowLocationModel model)
        {
            var photo = new Photo
            {
                CreatedOn = DateTime.Now,
            };
            using (var ms = new MemoryStream())
            {
                model.Photo.CopyTo(ms);
                photo.Content = ms.ToArray();
            }
            UnitOfWork.Photos.Insert(photo);
            var locationPhoto = new PhotoLocation
            {
                Photo = photo,
                LocationId = model.Id,
                Description = model.PhotoDescription
            };
            UnitOfWork.PhotoLocations.Insert(locationPhoto);
            UnitOfWork.SaveChanges();
        }

        public List<(string, byte[], int)> GetPhotosLocation(int locationid)
        {
            var photosLocation = UnitOfWork.PhotoLocations.Get().Where(x => x.LocationId == locationid).Include(x => x.Photo).ToList();
            return photosLocation.Select(x => (x.Description, x.Photo.Content, x.Photo.Id)).ToList();
            
        }

        public void DeletePhotoLocation(int photoId)
        {
            var photoLocation = UnitOfWork.PhotoLocations.Get().FirstOrDefault(x => x.PhotoId == photoId);
            var photo = UnitOfWork.Photos.Get().FirstOrDefault(x => x.Id == photoId);
            UnitOfWork.PhotoLocations.Delete(photoLocation);
            UnitOfWork.Photos.Delete(photo);
            UnitOfWork.SaveChanges();
        }

        public ShowEventPhotoModel PhotoEvent(int eventId)
        {
            var photosEvent = UnitOfWork.PhotoEvents.Get().Where(x => x.EventId == eventId).Include(x => x.Photo).ToList();
            var showEventPhotoModel = new ShowEventPhotoModel
            {
                Photos = photosEvent.Select(x => (x.Description, x.Photo.Content, x.PhotoId)).ToList(),
                Id = eventId,
                PhotoDescription = ""
            };
            return showEventPhotoModel;
        }

        public void AddPhotoEvent(ShowEventPhotoModel model)
        {
            addPhotoValidator.Validate(model).ThenThrow(model);
            var @event = UnitOfWork.Events.Get().FirstOrDefault(x => x.Id == model.Id);
            if (@event == null)
            {
                throw new Exception("Event not found");
            }
            if (@event.EndDate < DateTime.Now)
            {
                throw new UnauthorizedAccessException();
            }
            var photo = new Photo
            {
                CreatedOn = DateTime.Now,
            };
            using (var ms = new MemoryStream())
            {
                model.Photo.CopyTo(ms);
                photo.Content = ms.ToArray();
            }
            UnitOfWork.Photos.Insert(photo);
            var photoEvent = new PhotoEvent
            {
                Photo = photo,
                EventId = model.Id,
                Description = model.PhotoDescription
            };
            UnitOfWork.PhotoEvents.Insert(photoEvent);
            UnitOfWork.SaveChanges();
        }

        public void DeletePhotoEvent(int photoId)
        {
            var photoEvent = UnitOfWork.PhotoEvents.Get().FirstOrDefault(x => x.PhotoId == photoId);
            var photo = UnitOfWork.Photos.Get().FirstOrDefault(x => x.Id == photoId);
            UnitOfWork.PhotoEvents.Delete(photoEvent);
            UnitOfWork.Photos.Delete(photo);
            UnitOfWork.SaveChanges();
        }

        public string GetContentById(int id)
        {
            return "data:image/png;base64," + Convert.ToBase64String(UnitOfWork.Photos.Get().FirstOrDefault(x => x.Id == id).Content).ToString();
        }
    }
}
