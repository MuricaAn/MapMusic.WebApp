using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.Location.Validations;
using MapMusic.BusinessLogic.Implementation.PhotoImp;
using MapMusic.Common.Extensions;
using MapMusic.DataAccess;
using MapMusic.Entities.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Location
{
    public class LocationService : BaseService
    {
        private readonly EventService eventService;
        private readonly PhotoService photoService;
        private readonly CreateLocationValidator createLocationValidator;
        public LocationService(ServiceDependencies serviceDependencies, EventService eventService, PhotoService photoService) : base(serviceDependencies)
        {
            this.eventService = eventService;
            this.photoService = photoService;
            this.createLocationValidator = new CreateLocationValidator();
        }

        public void AddLocation (CreateLocationModel model)
        {
            createLocationValidator.Validate(model).ThenThrow(model);
            var location = new Entities.Entities.Location
            {
                Name = model.Name,
                Address = model.Address,
                Description = model.Description,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };

            UnitOfWork.Locations.Insert(location);
            UnitOfWork.SaveChanges();
        }

        public List<Entities.Entities.Location> GetLocation()
        {
            return UnitOfWork.Locations.Get().ToList();
        }

        public Entities.Entities.Location GetLocation(int locationId)
        {
            return UnitOfWork.Locations.Get().Include(x => x.Events).ToList().FirstOrDefault(x => x.Id == locationId);
            //return UnitOfWork.Locations.Get().ToList().FirstOrDefault(x => x.Id == locationId);
        }

        public List<Entities.Entities.Event> GetUpcomingEvents(int locationId)
        {
            var events = GetLocation(locationId).Events.Where(x => x.EndDate > DateTime.Now).ToList();
            return events;
        }

        public List<Entities.Entities.Event> GetPastEvents(int locationId)
        {
            var events = GetLocation(locationId).Events.Where(x => x.EndDate < DateTime.Now).ToList();
            return events;
        }

        public float GetRatingLocation(int locationId)
        {
            var events = UnitOfWork.Events.Get().Where(x => x.LocationId == locationId).ToList();
            if (events.Count == 0)
            {
                return 0;
            }
            float sum = 0;
            float count = 0;
            foreach (var @event in events)
            {
                var ratingEventLocation = UnitOfWork.Ratings.Get().Where(r => r.EventId == @event.Id).ToList();
                sum += ratingEventLocation.Sum(r => r.RatingLocation);
                count += ratingEventLocation.Count;
            }
            return sum / count;
        }

        public ShowLocationModel ShowLocation(int locationId)
        {
            //locationId = 1;
            var location = GetLocation(locationId);
            var showLocationModel = new ShowLocationModel
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Photos  = photoService.GetPhotosLocation(locationId),   
                Description = location.Description,
                Rating = GetRatingLocation(locationId),
                UpcomingEvents = GetUpcomingEvents(locationId).Select(x => eventService.EventToShowEventModel(x)).OrderBy(e => e.EndDate).ToList(),
                PastEvents = GetPastEvents(locationId).Select(x => eventService.EventToShowEventModel(x)).OrderByDescending(e => e.EndDate).ToList(),
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
            return showLocationModel;
        }

        public bool IsAnyLocationWithThisName(string name)
        {
            if (name == null)
            {
                return false;
            }
            var location = UnitOfWork.Locations.Get().FirstOrDefault(x => x.Name == name);
            if (location == null)
            {
                return false;
            }
            return true;
        }

        public bool IsAnyLocationWithThisAddress(string address)
        {
            if (address == null)
            {
                return false;
            }
            var location = UnitOfWork.Locations.Get().FirstOrDefault(x => x.Address == address);
            if (location == null)
            {
                return false;
            }
            return true;
        }

        public List<ShowEventModel> GetLocationUpcomingEvents(int page, int locationId)
        {
            return GetUpcomingEvents(locationId).Select(x => eventService.EventToShowEventModel(x)).OrderBy(e => e.EndDate).Skip((page - 1) * Constants.PageSize).Take(Constants.PageSize).ToList();
        }

        public List<ShowEventModel> GetLocationPastEvents(int page, int locationId)
        {
            return GetPastEvents(locationId).Select(x => eventService.EventToShowEventModel(x)).OrderByDescending(e => e.EndDate).Skip((page - 1) * Constants.PageSize).Take(Constants.PageSize).ToList();
        }
    }
}
