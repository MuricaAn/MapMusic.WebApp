using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.Location;
using MapMusic.Entities.Entities;
using MapMusic.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using MapMusic.BusinessLogic.Implementation.Rating.Models;
using MapMusic.BusinessLogic.Implementation.Event.Validations;
using MapMusic.Common.Extensions;
using FluentValidation;
using Azure;
using MapMusic.Entities.Constants;

namespace MapMusic.BusinessLogic.Implementation.Event
{
    public class EventService : BaseService
    {

        private readonly CreateEventValidator createEventValidator;
        private readonly EditEventValidator editEventValidator;
        public EventService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            this.createEventValidator = new CreateEventValidator();
            this.editEventValidator = new EditEventValidator();
        }

        public List<MusicType> GetMusicTypes()
        {
            return UnitOfWork.MusicTypes.Get().ToList();
        }

        public List<Artist> GetArtists()
        {
            return UnitOfWork.Artists.Get().ToList();
        }
        public void CreateEvent(CreateEventModel model)
        {
            createEventValidator.Validate(model).ThenThrow(model);
            var newEvent = new Entities.Entities.Event 
            { 
                Name = model.Name,
                Description = model.Description, 
                StartDate = (DateTimeOffset)model.StartDate,
                EndDate = (DateTimeOffset)model.EndDate,
                MusicTypeId = model.MusicTypeId, 
                OrganizerId = model.OrganizerId,
                LocationId = (int)model.LocationId,
                Price = model.Price
            };
            using (var ms = new MemoryStream())
            {
                model.ProfilePhoto.CopyTo(ms);
                newEvent.ProfilePhoto = ms.ToArray();
            }

            UnitOfWork.Events.Insert(newEvent);
            foreach (var artistId in model.ArtistsId)
            {
                var newEventArtist = new OrganizerArtistInvitation
                {
                    ArtistId = artistId,
                    OrganizerId = model.OrganizerId,
                    Event = newEvent,
                    OrganizerArtistInvitationStatusId = (int)OrganizerArtistInvitationStatusEnum.Pending
                };
                UnitOfWork.OrganizerArtistInvitations.Insert(newEventArtist);
            }
            UnitOfWork.SaveChanges();
        }

        public void UpdateEvent(EditEventModel model)
        {
            editEventValidator.Validate(model).ThenThrow(model);
            var @event = GetEventById(model.Id);
            if (model.NewProfilePhoto != null)
            {
                using (var ms = new MemoryStream())
                {
                    model.NewProfilePhoto.CopyTo(ms);
                    @event.ProfilePhoto = ms.ToArray();
                }
            }
            @event.Name = model.Name;
            @event.Description = model.Description;
            @event.StartDate = model.StartDate;
            @event.EndDate = model.EndDate;
            @event.MusicTypeId = model.MusicTypeId;
            @event.Price = model.Price;
            @event.LocationId = model.LocationId;
            UnitOfWork.Events.Update(@event);
            if (model.ArtistsId != null)
            {
                foreach (var artistId in model.ArtistsId)
                {
                    var newEventArtist = new OrganizerArtistInvitation
                    {
                        ArtistId = artistId,
                        OrganizerId = CurrentUser.Id,
                        Event = @event,
                        OrganizerArtistInvitationStatusId = (int)OrganizerArtistInvitationStatusEnum.Pending
                    };
                    UnitOfWork.OrganizerArtistInvitations.Insert(newEventArtist);
                }
            }
            UnitOfWork.SaveChanges();
        }
        public List<Entities.Entities.Event> GetEvents()
        {
            return UnitOfWork.Events.Get().Include(e => e.Ratings).ToList();
        }

        public List<Entities.Entities.Event> GetUpcomingEvents(int page, List<int> musicTypes, int? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            var nr = UnitOfWork.Events.Get().Where(x => x.EndDate > DateTime.Now
                                             && (maxPrice == null || x.Price < maxPrice)
                                             && (startDate == null || x.StartDate > startDate)
                                             && (endDate == null || x.StartDate < endDate)
                                             && (musicTypes.Count == 0 || musicTypes.Contains(x.MusicTypeId))).Count();
            return UnitOfWork.Events.Get().Where(x => x.EndDate > DateTime.Now 
                                             && (maxPrice == null || x.Price <maxPrice)
                                             && (startDate == null || x.StartDate > startDate)
                                             && (endDate ==null || x.StartDate < endDate)
                                             && (musicTypes.Count == 0 || musicTypes.Contains(x.MusicTypeId))).Skip((page-1)*Constants.PageSize).Take(Constants.PageSize).ToList();
        }

        public List<Entities.Entities.Event> GetPastEvents(int page, List<int> musicTypes, int? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            var nr = UnitOfWork.Events.Get().Where(x => x.EndDate < DateTime.Now
                                             && (maxPrice == null || x.Price < maxPrice)
                                             && (startDate == null || x.StartDate > startDate)
                                             && (endDate == null || x.StartDate < endDate)
                                             && (musicTypes.Count == 0 || musicTypes.Contains(x.MusicTypeId))).Count();
            return UnitOfWork.Events.Get().Where(x => x.EndDate < DateTime.Now
                                             && (maxPrice == null || x.Price < maxPrice)
                                             && (startDate == null || x.StartDate > startDate)
                                             && (endDate == null || x.StartDate < endDate)
                                             && (musicTypes.Count == 0 || musicTypes.Contains(x.MusicTypeId))).Skip((page - 1) * Constants.PageSize).Take(Constants.PageSize).ToList();
        }

        public MusicType GetMusicType(int musicTypeId)
        {
            return UnitOfWork.MusicTypes.Get().FirstOrDefault(x => x.Id == musicTypeId);
        }

        public Entities.Entities.Event? GetEventById(int eventId)
        {
            return UnitOfWork.Events.Get().FirstOrDefault(x => x.Id == eventId);
        }

        public List<Artist> GetArtists(int eventId)
        {
            var e = UnitOfWork.Events.Get().Include(e => e.Artists).Where(e => e.Id == eventId).FirstOrDefault();
            if (e == null)
            {
                return new List<Artist>();
            }
            return e.Artists.ToList();
        }
        public ShowEventModel ShowEvent(int eventId)
        {
            return EventToShowEventModel(GetEvents().FirstOrDefault(x => x.Id == eventId));
        }

        public EditEventModel EditEvent(int eventId)
        {
            var @event = GetEventById(eventId);
            @event.Artists = GetArtists(eventId);
            @event.Location = GetLocation(@event.LocationId);
            var editEvent = new EditEventModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                LocationName = @event.Location.Name,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                MusicTypeId = @event.MusicTypeId,
                OrganizerId = @event.OrganizerId,
                LocationId = @event.LocationId,
                ProfilePhoto = @event.ProfilePhoto,
                Price = @event.Price,
                ArtistsId = @event.Artists.Select(x => x.Id).ToList()
            };
            return editEvent;
        }

        public Entities.Entities.Location GetLocation(int locationId)
        {
            //return UnitOfWork.Locations.Get().Include(x => x.Events).ToList().FirstOrDefault(x => x.Id == locationId);
            return UnitOfWork.Locations.Get().FirstOrDefault(x => x.Id == locationId);
        }
        public Entities.Entities.Organizer GetOrganizer(int organizerId)
        {
            return UnitOfWork.Organizers.Get().Include(o => o.Photo).SingleOrDefault(u => u.Id == organizerId);
        }

        public ShowEventModel EventToShowEventModel(Entities.Entities.Event eventt)
        {
            eventt.Location = GetLocation(eventt.LocationId);
            eventt.Organizer = GetOrganizer(eventt.OrganizerId);
            eventt.Artists = GetArtists(eventt.Id);
            eventt.MusicType = GetMusicType(eventt.MusicTypeId);
            var showEventModel = new ShowEventModel
            {
                Id = eventt.Id,
                Name = eventt.Name,
                MusicType = eventt.MusicType.Name,
                Description = eventt.Description,
                Price = eventt.Price,
                Artists = eventt.Artists.Select(x => (x.Id, x.StageName)).ToList(),
                Location = new ShowLocationModel
                {
                    Id = eventt.Location.Id,
                    Name = eventt.Location.Name,
                    Address = eventt.Location.Address,
                    Description = eventt.Location.Description,
                    Longitude = eventt.Location.Longitude,
                    Latitude = eventt.Location.Latitude
                },
                Ratings = new List<RatingOnEventModel>(),
                Organizer = (eventt.Organizer.Id, eventt.Organizer.FullName),
                StartDate = eventt.StartDate,
                EndDate = eventt.EndDate,
                ProfilePhoto = eventt.ProfilePhoto
            };
            foreach (var rating in eventt.Ratings)
            {
                showEventModel.Ratings.Add(new RatingOnEventModel
                {
                    FirstName = GetUserById(rating.UserId).FirstName,
                    LastName = GetUserById(rating.UserId).LastName,
                    Comment = rating.Comment,
                    OverallRating = (float)(rating.RatingLocation + rating.RatingOrganization)/2
                });
            }
            return showEventModel;
        }

        public void DeleteEvent(int eventId)
        {
            var @event = GetEventById(eventId);
            UnitOfWork.Events.Delete(@event);
            UnitOfWork.SaveChanges();
        }

        public List<ShowEventModel> GetUpcommingEvents()
        {
            var events = GetEvents().Where(x => x.EndDate > DateTime.Now && (x.EndDate - DateTime.Now).Days <=7 ).ToList();
            events = events
                .OrderBy(x => x.StartDate).ToList()
                .GroupBy(x => x.LocationId)
                .Select(@event => @event.First())
                .ToList();
            var showEvents = new List<ShowEventModel>();
            foreach (var @event in events)
            {
                showEvents.Add(EventToShowEventModel(@event));
            }
            return showEvents;
        }


        public void AddFavouriteEvent(int eventId, int userId)
        {
            var user = GetUserById(userId);
            var @event = GetEventById(eventId);
            user.Events.Add(@event);
            UnitOfWork.SaveChanges();

        }
        
        public void RemoveFavouriteEvent(int eventId, int userId)
        {
            var user = GetUserById(userId);
            var @event = GetEventById(eventId);
            user.Events.Remove(@event);
            UnitOfWork.SaveChanges();
        }

        public bool GetFavouriteEvents(int userId, int eventId)
        {
            var user = GetUserById(userId);
            var nr = user.Events.Where(x => x.Id == eventId).Count();
            return nr == 1;
        }


        public User GetUserById(int userId)
        {
            return UnitOfWork.Users.Get().Include(u => u.Events).FirstOrDefault(x => x.Id == userId);
        }

        //public List<EventSearchBarModel> GetEventsForSearchBar(string search) { 

        //    var events = GetEvents().Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
        //    var eventSearchBarModels = new List<EventSearchBarModel>();
        //    var i = 0;
        //    foreach(var @event in events)
        //    {
        //        eventSearchBarModels.Add(new EventSearchBarModel
        //        {
        //            Id = @event.Id,
        //            Text = @event.Name,
        //            ProfilePhoto = @event.ProfilePhoto,
        //            EntityType = 
        //        });
        //        if (++i > 2 )
        //        {
        //            break;
        //        }
        //    }
        //    return eventSearchBarModels;
        //}

    }
}
