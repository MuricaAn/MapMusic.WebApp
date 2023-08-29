using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using MapMusic.BusinessLogic.Implementation.Account.Validations;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using MapMusic.BusinessLogic.Implementation.Organizer;
using MapMusic.BusinessLogic.Implementation.Organizer.Models;
using MapMusic.Common.Extensions;
using MapMusic.Entities.Entities;
using MapMusic.Entities.Enums;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace MapMusic.BusinessLogic.Implementation.Account
{
    public class AccountService : BaseService
    {
        private readonly RegisterArtistValidator registerArtistValidator;
        private readonly RegisterUserValidator registerUserValidator;
        private readonly RegisterOrganizerValidator registerOrganizerValidator;
        private readonly EventService eventService;
        private readonly OrganizerService organizerService;
        private readonly MyUserValidator myUserValidator;
        private readonly MyOrganizerValidator myOrganizerValidator;
        private readonly MyArtistValidator myArtistValidator;
        public AccountService(ServiceDependencies serviceDependencies, EventService eventService, OrganizerService organizerService) : base(serviceDependencies)
        {
            this.eventService = eventService;
            this.organizerService = organizerService;
            registerArtistValidator = new RegisterArtistValidator();
            registerUserValidator = new RegisterUserValidator();
            registerOrganizerValidator = new RegisterOrganizerValidator();
            myUserValidator = new MyUserValidator();
            myOrganizerValidator = new MyOrganizerValidator();
            myArtistValidator = new MyArtistValidator();
        }

        public string GetArtistEmailById(int id)
        {
            return UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Id == UnitOfWork.Artists.Get().SingleOrDefault(a => a.Id == id).CredentialId).Email;
        }
        public string GetOrganizerEmailById(int id)
        {
            return UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Id == UnitOfWork.Organizers.Get().SingleOrDefault(a => a.Id == id).CredentialId).Email;
        }
        public Credential? GetByEmailAndPassword(LoginModel loginModel)
        {
            return UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Email == loginModel.Email && u.PasswordHash == loginModel.Password.Sha256());
        }

        public User? GetUserByCredentialId(int credentialId)
        {
            return UnitOfWork.Users.Get().SingleOrDefault(u => u.CredentialId == credentialId);
        }

        public Artist? GetArtistByCredentialId (int credentialId)
        {
            return UnitOfWork.Artists.Get().SingleOrDefault(u => u.CredentialId == credentialId);
        }

        public Entities.Entities.Organizer? GetOrganizerByCredentialId(int credentialId)
        {
            return UnitOfWork.Organizers.Get().SingleOrDefault(u => u.CredentialId == credentialId);
        }

        public Credential GetCredentialByEmail(string email)
        {
            return UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Email == email);
        }

        public void RegisterUser(RegisterUserModel registerUserModel)
        {
            registerUserValidator.Validate(registerUserModel).ThenThrow(registerUserModel);
            var credential = new Credential
            {
                Email = registerUserModel.Email,
                PasswordHash = registerUserModel.Password.Sha256(),
            };

            var user = new User
            {
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName,
                BirthDate = registerUserModel.BirthDay,
                RoleId = (int)RoleType.User,
                Credential = credential,
                PhotoId = 2
            };
            UnitOfWork.Users.Insert(user);
            UnitOfWork.SaveChanges();
        }

        public void RegisterArtist(RegisterArtistModel registerArtistModel)
        {
            var validationResult = registerArtistValidator.Validate(registerArtistModel);
            if (!validationResult.IsValid)
            {
                registerArtistModel.ArtistTypeList = GetArtistTypes().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
                SelectListItem newItem = new SelectListItem { Value = "0", Text = "Choose your artist type" };
                registerArtistModel.ArtistTypeList.Add(newItem);
                validationResult.ThenThrow(registerArtistModel);
            }
            var credential = new Credential
            {
                Email = registerArtistModel.Email,
                PasswordHash = registerArtistModel.Password.Sha256(),
            };

            var artist = new Artist
            {
                StageName = registerArtistModel.StageName,
                ArtistTypeId = registerArtistModel.ArtistType,
                Credential = credential,
                PhotoId = 2
            };
            UnitOfWork.Artists.Insert(artist);
            UnitOfWork.SaveChanges();
        }

        public void RegisterOrganizer(RegisterOrganizerModel registerOrganizerModel)
        {

            registerOrganizerValidator.Validate(registerOrganizerModel).ThenThrow(registerOrganizerModel);
            var organizerRequest = new OrganizerRequest
            {
                FullName = registerOrganizerModel.FullName,
                Description = registerOrganizerModel.Description,
                Email = registerOrganizerModel.Email,
                Password = registerOrganizerModel.Password.Sha256(),
                OrganizerRequestStatusId = (int)OrganizerRequestStatusEnum.Pending
            };
            UnitOfWork.OrganizerRequests.Insert(organizerRequest);
            UnitOfWork.SaveChanges();
        }

        
        public List<ArtistType> GetArtistTypes()
        {
            return UnitOfWork.ArtistTypes.Get().ToList();
        }

        public Entities.Entities.Organizer GetOrganizer(int organizerId)
        {
            return UnitOfWork.Organizers.Get().Include(o => o.Photo).SingleOrDefault(u => u.Id == organizerId);
        }

        public Artist GetArtist(int artistId)
        {
            return UnitOfWork.Artists.Get().Include(u => u.Photo).ToList().SingleOrDefault(u => u.Id == artistId);
        }

        public User GetUser(int userId)
        {
            return UnitOfWork.Users.Get().Include(u => u.Photo).SingleOrDefault(u => u.Id == userId);
        }

        public Photo GetPhotoById (int photoId)
        {
            return UnitOfWork.Photos.Get().SingleOrDefault(u => u.Id == photoId);
        }

        public MyUserProfileModel GetMyUserProfileModelById(int userId)
        {
            var user = GetUser(userId);
            var myUserProfileModel = new MyUserProfileModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            if (user.PhotoId == null)
            {
                user.PhotoId = 2;
            }
            myUserProfileModel.ProfilePhotoId = user.PhotoId;


            return myUserProfileModel;
        }

        public bool IsUserPasswordCorrect(int userId, string password)
        {
            var user = GetUser(userId);
            user.Credential = UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Id == user.CredentialId);
            return user.Credential.PasswordHash == password.Sha256();
        }

        public bool IsArtistPasswordCorrect(int artistId, string password)
        {
            var artist = GetArtist(artistId);
            artist.Credential = UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Id == artist.CredentialId);
            return artist.Credential.PasswordHash == password.Sha256();
        }

        public bool IsOrganizerPasswordCorrect(int organizerId, string password)
        {
            var organizer = GetOrganizer(organizerId);
            organizer.Credential = UnitOfWork.Credentials.Get().SingleOrDefault(u => u.Id == organizer.CredentialId);
            return organizer.Credential.PasswordHash == password.Sha256();
        }

        public void UpdateMyUserProfile(MyUserProfileModel myUserProfileModel)
        {
            myUserValidator.Validate(myUserProfileModel).ThenThrow(myUserProfileModel);
            var user = GetUser(myUserProfileModel.Id);
            if (myUserProfileModel.NewProfilePhoto!=null)
            {
                var photo = new Photo
                {
                    CreatedOn = DateTime.Now
                };
                using (var ms = new MemoryStream())
                {
                    myUserProfileModel.NewProfilePhoto.CopyTo(ms);
                    photo.Content = ms.ToArray();
                }
                UnitOfWork.Photos.Insert(photo);
                user.Photo = photo;
            }
            if (myUserProfileModel.NewPassword != null)
            {
                user.Credential.PasswordHash = myUserProfileModel.NewPassword.Sha256();
            }
            UnitOfWork.Users.Update(user);
            UnitOfWork.SaveChanges();
        }

        public void UpdateMyArtistProfile(MyArtistProfileModel myArtistProfileModel)
        {
            myArtistValidator.Validate(myArtistProfileModel).ThenThrow(myArtistProfileModel);
            var artist = GetArtist(myArtistProfileModel.Id);
            artist.Description = myArtistProfileModel.Description;
            if (myArtistProfileModel.StageName != null)
            {
                artist.StageName = myArtistProfileModel.StageName;
                CurrentUser.FullName = myArtistProfileModel.StageName;

            }
            if (myArtistProfileModel.NewProfilePhoto != null)
            {
                var photo = new Photo
                {
                    CreatedOn = DateTime.Now
                };
                using (var ms = new MemoryStream())
                {
                    myArtistProfileModel.NewProfilePhoto.CopyTo(ms);
                    photo.Content = ms.ToArray();
                }
                UnitOfWork.Photos.Insert(photo);
                artist.Photo = photo;
            }
            if (myArtistProfileModel.NewPassword != null)
            {
                artist.Credential.PasswordHash = myArtistProfileModel.NewPassword.Sha256();
            }
            UnitOfWork.Artists.Update(artist);
            UnitOfWork.SaveChanges();
        }

        public void UpdateOrganizerProfile(MyOrganizerProfileModel myOrganizerProfileModel)
        {
            myOrganizerValidator.Validate(myOrganizerProfileModel).ThenThrow(myOrganizerProfileModel);
            var organizer = GetOrganizer(myOrganizerProfileModel.Id);
            organizer.Description = myOrganizerProfileModel.Description;
            if (myOrganizerProfileModel.FullName != null)
            {
                organizer.FullName = myOrganizerProfileModel.FullName;
                CurrentUser.FullName = myOrganizerProfileModel.FullName;
            }
            if (myOrganizerProfileModel.NewProfilePhoto != null)
            {
                var photo = new Photo
                {
                    CreatedOn = DateTime.Now
                };
                using (var ms = new MemoryStream())
                {
                    myOrganizerProfileModel.NewProfilePhoto.CopyTo(ms);
                    photo.Content = ms.ToArray();
                }
                UnitOfWork.Photos.Insert(photo);
                organizer.Photo = photo;
            }
            if (myOrganizerProfileModel.NewPassword != null)
            {
                organizer.Credential.PasswordHash = myOrganizerProfileModel.NewPassword.Sha256();
            }
            UnitOfWork.Organizers.Update(organizer);
            UnitOfWork.SaveChanges();
        }

        public MyArtistProfileModel GetMyArtistProfileModelById(int artistId)
        {
            var artist = GetArtist(artistId);
            var myArtistProfileModel = new MyArtistProfileModel
            {
                Id = artist.Id,
                StageName = artist.StageName,
                Description = artist.Description
            };
            if (artist.PhotoId == null)
            {
                artist.PhotoId = 2;
            }
            myArtistProfileModel.ProfilePhotoId = artist.PhotoId;

            return myArtistProfileModel;
        }

        public MyOrganizerProfileModel GetMyOrganizerProfileModelById(int organizerId)
        {
            var organizer = GetOrganizer(organizerId);
            var myOrganizerProfileModel = new MyOrganizerProfileModel
            {
                Id = organizer.Id,
                FullName = organizer.FullName,
                Description = organizer.Description
            };
            if (organizer.PhotoId == null)
            {
                organizer.PhotoId = 2;
            }
            myOrganizerProfileModel.ProfilePhotoId = organizer.PhotoId;

            return myOrganizerProfileModel;
        }

        
        public List<Entities.Entities.Event> GetUpcomingEvents(int artistId)
        {
            return UnitOfWork.Events.Get().Include(e => e.Artists).Where(e => e.Artists.Contains(GetArtist(artistId)) && e.EndDate > DateTime.Now).ToList();
        }

        public List<Entities.Entities.Event> GetPastEvents(int artistId)
        {
            return UnitOfWork.Events.Get().Include(e => e.Artists).Where(e => e.Artists.Contains(GetArtist(artistId)) && e.EndDate < DateTime.Now).ToList();
        }

        public ShowArtistModel ShowArtist(int artistId)
        {
            var artist = UnitOfWork.Artists.Get().Include(a => a.ArtistRatings).SingleOrDefault(a => a.Id == artistId);
            var showArtistiModel = new ShowArtistModel {
                Id = artist.Id,
                StageName = artist.StageName,
                Description = artist.Description,
                ProfilePhoto = GetPhotoById(artist.PhotoId),
                UpcomingEvents = GetUpcomingEvents(artistId).Select(x => eventService.EventToShowEventModel(x)).OrderBy(e => e.EndDate).ToList(),
                PastEvents = GetPastEvents(artistId).Select(x => eventService.EventToShowEventModel(x)).OrderByDescending(e => e.EndDate).ToList()
            };

            if (artist.ArtistRatings.Count > 0)
            {
                showArtistiModel.Rating = (float)(artist.ArtistRatings.Select(r => r.Rating)).Average();
            }
            else
            {
                showArtistiModel.Rating = 0;
            }
            return showArtistiModel;
        }

        public ShowOrganizerModel ShowOrganizer(int organizerId)
        {
            var organizer = UnitOfWork.Organizers.Get().SingleOrDefault(a => a.Id == organizerId);
            var showArtistiModel = new ShowOrganizerModel
            {
                Id = organizer.Id,
                FullName = organizer.FullName,
                Description = organizer.Description,
                ProfilePhoto = GetPhotoById(organizer.PhotoId),
                Rating = organizerService.GetRatingOrganizer(organizerId),
                UpcomingEvents = organizerService.GetUpcomingEvents(organizerId).Select(x => eventService.EventToShowEventModel(x)).OrderBy(e => e.EndDate).ToList(),
                PastEvents = organizerService.GetPastEvents(organizerId).Select(x => eventService.EventToShowEventModel(x)).OrderByDescending(e => e.EndDate).ToList()
            };
            return showArtistiModel;
        }
        public User GetUserAndFavourites(int userId)
        {
            return UnitOfWork.Users.Get().Include(u => u.Events).SingleOrDefault(u => u.Id == userId);
        }
        public UserFavourites GetMyFavourites(int userId)
        {
            var user = GetUserAndFavourites(userId);
            var userFavourites = new UserFavourites
            {
                FavouritesEvents = user.Events.Select(e => eventService.EventToShowEventModel(e)).OrderByDescending(e => e.EndDate).ToList(),
            };
            return userFavourites;
        }
    }
}
