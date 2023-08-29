using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Rating.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Rating
{
    public class RatingService : BaseService
    {
        private readonly EventService eventService;
        public RatingService(ServiceDependencies serviceDependencies, EventService eventService) : base(serviceDependencies)
        {
            this.eventService = eventService;
        }

        public GiveRatingModel GetGiveRatingModel(int eventId)
        {
            var @event = eventService.GetEventById(eventId);
            if (@event.EndDate > DateTime.Now)
            {
                throw new UnauthorizedAccessException();
            }
            var artists = eventService.GetArtists(eventId);
            var giveRatingModel = new GiveRatingModel
            {
                EventName = @event.Name,
                LocationName = eventService.GetLocation(@event.LocationId).Name,
                OrganizerName = eventService.GetOrganizer(@event.OrganizerId).FullName,
                EventId = eventId,
                UserId = CurrentUser.Id,
                PresentArtists = artists,
                IsRated = false
            };
            if (UnitOfWork.Ratings.Get().Where(r => r.EventId == eventId && r.UserId == CurrentUser.Id).Any())
            {

                var rating = UnitOfWork.Ratings.Get().Include(rating => rating.ArtistRatings).Where(r => r.EventId == eventId && r.UserId == CurrentUser.Id).FirstOrDefault();
                giveRatingModel.IsRated = true;
                giveRatingModel.RatingLocation = rating.RatingLocation;
                giveRatingModel.RatingOrganization = rating.RatingOrganization;
                giveRatingModel.Comment = rating.Comment;
                giveRatingModel.RatingsForArtists = rating.ArtistRatings.Select(ar => ar.Rating).ToList();
                giveRatingModel.ArtistsId = rating.ArtistRatings.Select(ar => ar.ArtistId).ToList();

            }
            return giveRatingModel;
            
        }

        public void CreateRating(GiveRatingModel model)
        {
            var artist = eventService.GetArtists(model.EventId);
            var rating = new Entities.Entities.Rating
            {
                EventId = model.EventId,
                UserId = CurrentUser.Id,
                RatingLocation = model.RatingLocation,
                RatingOrganization = model.RatingOrganization,
                Comment = model.Comment
            };
            UnitOfWork.Ratings.Insert(rating);
            for (int i = 0; i < model.ArtistsId.Count; i++)
            {
                var artistRating = new Entities.Entities.ArtistRating
                {
                    ArtistId = model.ArtistsId[i],
                    RatingNavigation = rating,
                    Rating = model.RatingsForArtists[i]
                };
                UnitOfWork.ArtistRatings.Insert(artistRating);
            }
                UnitOfWork.SaveChanges();
        }

        public void UpdateRating(GiveRatingModel model)
        {
            var rating = UnitOfWork.Ratings.Get().Include(rating => rating.ArtistRatings).Where(r => r.EventId == model.EventId && r.UserId == CurrentUser.Id).FirstOrDefault();
            rating.RatingLocation = model.RatingLocation;
            rating.RatingOrganization = model.RatingOrganization;
            rating.Comment = model.Comment;
            for (int i= 0; i< model.ArtistsId.Count; i++)
            {
                var artistRanting = rating.ArtistRatings.Where(ar => ar.ArtistId == model.ArtistsId[i]).FirstOrDefault();
                artistRanting.Rating = model.RatingsForArtists[i];
            }
            UnitOfWork.Ratings.Update(rating);
            UnitOfWork.SaveChanges();
        }
    }
}
