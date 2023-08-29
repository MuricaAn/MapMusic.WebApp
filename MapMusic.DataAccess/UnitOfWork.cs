using MapMusic.Common;
using MapMusic.Entities;
using MapMusic.Entities.Entities;

namespace MapMusic.DataAccess
{
    public class UnitOfWork
    {
        private readonly MapMusicContext Context;

        public UnitOfWork(MapMusicContext context)
        {
            this.Context = context;
        }
        private IRepository<Artist> artists;
        public IRepository<Artist> Artists => artists ?? (artists = new BaseRepository<Artist>(Context));

        private IRepository<ArtistRating> artistRatings;
        public IRepository<ArtistRating> ArtistRatings => artistRatings ?? (artistRatings = new BaseRepository<ArtistRating>(Context));

        private IRepository<ArtistType> artistTypes;
        public IRepository<ArtistType> ArtistTypes => artistTypes ?? (artistTypes = new BaseRepository<ArtistType>(Context));

        private IRepository<BlockedEmail> blockedEmails;
        public IRepository<BlockedEmail> BlockedEmails => blockedEmails ?? (blockedEmails = new BaseRepository<BlockedEmail>(Context));

        private IRepository<Credential> credentials;
        public IRepository<Credential> Credentials => credentials ?? (credentials = new BaseRepository<Credential>(Context));

        private IRepository<Event> events;
        public IRepository<Event> Events => events ?? (events = new BaseRepository<Event>(Context));

        private IRepository<Location> locations;
        public IRepository<Location> Locations => locations ?? (locations = new BaseRepository<Location>(Context));

        private IRepository<MusicType> musicTypes;
        public IRepository<MusicType> MusicTypes => musicTypes ?? (musicTypes = new BaseRepository<MusicType>(Context));

        private IRepository<Organizer> organizers;
        public IRepository<Organizer> Organizers => organizers ?? (organizers = new BaseRepository<Organizer>(Context));

        private IRepository<OrganizerArtistInvitation> organizerArtistInvitations;
        public IRepository<OrganizerArtistInvitation> OrganizerArtistInvitations => organizerArtistInvitations ?? (organizerArtistInvitations = new BaseRepository<OrganizerArtistInvitation>(Context));

        private IRepository<OrganizerArtistInvitationStatus> organizerArtistInvitationStatuses;
        public IRepository<OrganizerArtistInvitationStatus> OrganizerArtistInvitationStatuses => organizerArtistInvitationStatuses ?? (organizerArtistInvitationStatuses = new BaseRepository<OrganizerArtistInvitationStatus>(Context));
        
        private IRepository<OrganizerRequest> organizerRequests;
        public IRepository<OrganizerRequest> OrganizerRequests => organizerRequests ?? (organizerRequests = new BaseRepository<OrganizerRequest>(Context));

        private IRepository<OrganizerRequestStatus> organizerRequestStatuses;
        public IRepository<OrganizerRequestStatus> OrganizerRequestStatuses => organizerRequestStatuses ?? (organizerRequestStatuses = new BaseRepository<OrganizerRequestStatus>(Context));

        private IRepository<Photo> photos;
        public IRepository<Photo> Photos => photos ?? (photos = new BaseRepository<Photo>(Context));

        private IRepository<PhotoEvent> photoEvents;
        public IRepository<PhotoEvent> PhotoEvents => photoEvents ?? (photoEvents = new BaseRepository<PhotoEvent>(Context));

        private IRepository<PhotoLocation> photoLocations;
        public IRepository<PhotoLocation> PhotoLocations => photoLocations ?? (photoLocations = new BaseRepository<PhotoLocation>(Context));

        private IRepository<Rating> ratings;
        public IRepository<Rating> Ratings => ratings ?? (ratings = new BaseRepository<Rating>(Context));

        private IRepository<Role> roles;
        public IRepository<Role> Roles => roles ?? (roles = new BaseRepository<Role>(Context));

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<VwSearcheableEntity> vwSearcheableEntities;
        public IRepository<VwSearcheableEntity> VwSearcheableEntities => vwSearcheableEntities ?? (vwSearcheableEntities = new BaseRepository<VwSearcheableEntity>(Context));

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
