using AutoMapper;
using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Organizer.Mappings;
using MapMusic.BusinessLogic.Implementation.Organizer.Models;
using MapMusic.Common.Extensions;
using MapMusic.Entities.Entities;
using MapMusic.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Organizer
{
    public class OrganizerService : BaseService
    {
        private readonly IMapper _toOrganizerRequestShow;
        private readonly EventService eventService;
        public OrganizerService(ServiceDependencies serviceDependencies, IMapper toOrganizerRequestShow, EventService eventService) : base(serviceDependencies)
        { 
            _toOrganizerRequestShow = toOrganizerRequestShow;
            this.eventService = eventService;
        }
        public List<OrganizerRequestShow> GetOrganizerRequests()
        {
            var organizerRequests = UnitOfWork.OrganizerRequests.Get().ToList(); 
            return  _toOrganizerRequestShow.Map<List<OrganizerRequest>, List<OrganizerRequestShow>>(organizerRequests);
        }

        public OrganizerRequest? GetOrganizerRequestById(int organizerRequestId)
        {
            return UnitOfWork.OrganizerRequests.Get().SingleOrDefault(o => o.Id == organizerRequestId);
        }

        
        public void AcceptOrganizerRequest(int organizerRequestId)
        {
            var organizerRequest = UnitOfWork.OrganizerRequests.Get().SingleOrDefault(o => o.Id == organizerRequestId);
            if (organizerRequest != null)
            {
                var credential = new Credential
                {
                    Email = organizerRequest.Email,
                    PasswordHash = organizerRequest.Password,
                };

                var organizer = new Entities.Entities.Organizer
                {
                    FullName = organizerRequest.FullName,
                    Description = organizerRequest.Description,
                    Credential = credential,
                    PhotoId = 2
                };
                UnitOfWork.Organizers.Insert(organizer);
                organizerRequest.OrganizerRequestStatusId = (int)OrganizerRequestStatusEnum.Accepted;
                UnitOfWork.SaveChanges();
            }
        }

        public void RejectOrganizerRequest(int organizerRequestId)
        {
            var organizerRequest = GetOrganizerRequestById(organizerRequestId);
            organizerRequest.OrganizerRequestStatusId = (int)Entities.Enums.OrganizerRequestStatusEnum.Rejected;
            UnitOfWork.OrganizerRequests.Update(organizerRequest);
            UnitOfWork.SaveChanges();
        }

        public void AcceptOrganizerArtistInvitation(int organizerArtistRequestId)
        {
            var organizerArtistInvitation = GetOrganizerArtistInvitationById(organizerArtistRequestId);
            if (organizerArtistInvitation != null)
            {
                organizerArtistInvitation.OrganizerArtistInvitationStatusId = (int)OrganizerArtistInvitationStatusEnum.Accepted;
                organizerArtistInvitation.Event = eventService.GetEventById(organizerArtistInvitation.EventId);
                organizerArtistInvitation.Artist = UnitOfWork.Artists.Get().SingleOrDefault(a => a.Id == organizerArtistInvitation.ArtistId);
                organizerArtistInvitation.Artist.Events.Add(organizerArtistInvitation.Event);
                UnitOfWork.SaveChanges();
            }
        }

        public void RejectOrganizerArtistInvitation(int organizerArtistRequestId)
        {
            var organizerArtistInvitation = GetOrganizerArtistInvitationById(organizerArtistRequestId);
            if (organizerArtistInvitation != null)
            {
                organizerArtistInvitation.OrganizerArtistInvitationStatusId = (int)OrganizerArtistInvitationStatusEnum.Rejected;
                UnitOfWork.SaveChanges();
            }
        }

        public OrganizerArtistInvitation GetOrganizerArtistInvitationById(int organizerArtistRequestId)
        {
            return UnitOfWork.OrganizerArtistInvitations.Get().ToList().SingleOrDefault(o => o.Id == organizerArtistRequestId);
        }

        public List<OrganizerArtistInvitation>? GetEventInvitations(int organizerArtistInvitationId)
        {
            return UnitOfWork.OrganizerArtistInvitations.Get().Where(o => o.ArtistId == organizerArtistInvitationId).ToList();
        }

        public float GetRatingOrganizer(int organizerId)
        {
            var organizer = UnitOfWork.Organizers.Get().SingleOrDefault(o => o.Id == organizerId);
            if (organizer != null)
            {
                var events = UnitOfWork.Events.Get().Where(e => e.OrganizerId == organizerId).ToList();
                if (events.Count < 0)
                {
                    var rating = 0;
                    return rating;
                }
                float sum = 0;
                float count = 0;
                foreach(var @event in events)
                {
                    var ratingEventOrganizer = UnitOfWork.Ratings.Get().Where(r => r.EventId == @event.Id).ToList();
                    sum += ratingEventOrganizer.Sum(r => r.RatingOrganization);
                    count += ratingEventOrganizer.Count;
                }
                return sum/count;
            }
            return 0;
        }

        public List<Entities.Entities.Event> GetUpcomingEvents(int organizerId)
        {
            return UnitOfWork.Events.Get().Where(e => e.OrganizerId == organizerId && e.EndDate > DateTime.Now).ToList();
        }

        public List<Entities.Entities.Event> GetPastEvents(int organizerId)
        {
            return UnitOfWork.Events.Get().Where(e => e.OrganizerId == organizerId && e.EndDate < DateTime.Now).ToList();
        }

    }
}
