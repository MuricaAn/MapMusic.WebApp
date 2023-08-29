using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.WebApp.Code;
using MapMusic.Common.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using MapMusic.Entities.Enums;
using MapMusic.BusinessLogic.Implementation.Account.Validations;
using MapMusic.Common.Extensions;
using MapMusic.Entities.Entities;
using MapMusic.BusinessLogic.Implementation.Organizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MapMusic.WebApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountService accountService;
        private readonly RegisterArtistValidator registerArtistValidator;
        private readonly OrganizerService organizerService;

        public AccountController(ControllerDependencies dependencies, AccountService accountService, OrganizerService organizerService) : base(dependencies)
        {
            this.accountService = accountService;
            this.organizerService = organizerService;
            registerArtistValidator = new RegisterArtistValidator();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var credential = accountService.GetByEmailAndPassword(model);
            if (credential == null)
                return Unauthorized();
            var user = accountService.GetUserByCredentialId(credential.Id);
            var artist = accountService.GetArtistByCredentialId(credential.Id);
            var organizer = accountService.GetOrganizerByCredentialId(credential.Id);
            var claims = new List<Claim>();

            var nameIdentifierClaimValue = user?.Id.ToString() ?? artist?.Id.ToString() ?? organizer?.Id.ToString();
            var roleClaimValue = user != null
                ? ((int)RoleType.User).ToString()
                : artist != null
                    ? ((int)RoleType.Artist).ToString()
                    : organizer != null
                        ? ((int)RoleType.Organizer).ToString()
                        : string.Empty;
            var nameClaimValue = user != null
                ? $"{user.FirstName} {user.LastName}"
                : artist != null
                    ? artist.StageName
                    : organizer != null
                        ? organizer.FullName
                        : string.Empty;

            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, nameIdentifierClaimValue),
                new Claim(ClaimTypes.Name, nameClaimValue),
                new Claim(ClaimTypes.Role, roleClaimValue),
            });

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return LocalRedirect(model.ReturnUrl);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterUserModel model)
        {
            var credential = accountService.GetCredentialByEmail(model.Email);
            if (credential != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email already exists");
                return View(model);
            }
            accountService.RegisterUser(model);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult CreateArtist()
        {
            var model = new RegisterArtistModel
            {
                ArtistTypeList = accountService.GetArtistTypes().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList()
                
            };
            SelectListItem newItem = new SelectListItem { Value = "0", Text = "Choose your artist type" };
            model.ArtistTypeList.Add(newItem);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist(RegisterArtistModel model)
        {
            var credential = accountService.GetCredentialByEmail(model.Email);
            if (credential != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email already exists");
                model.ArtistTypeList = accountService.GetArtistTypes().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
                SelectListItem newItem = new SelectListItem { Value = "0", Text = "Choose your artist type" };
                model.ArtistTypeList.Add(newItem);
                return View(model);
            }
            accountService.RegisterArtist(model);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult CreateOrganizer()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganizer(RegisterOrganizerModel model)
        {
            var credential = accountService.GetCredentialByEmail(model.Email);
            if (credential != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email already exists");
                return View(model);
            }
            accountService.RegisterOrganizer(model);
            return Redirect("/");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult MyUserProfile(int userId)
        {
            if (userId == CurrentUser.Id)
            {
                var user = accountService.GetMyUserProfileModelById(userId);
                return View(user);
            }
            throw new UnauthorizedAccessException();

        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult MyUserProfile(MyUserProfileModel model)
        {
            
            if (model.Id == CurrentUser.Id)
            {
                if (model.CurrentPassword != null && accountService.IsUserPasswordCorrect(model.Id, model.CurrentPassword))
                {
                    accountService.UpdateMyUserProfile(model);
                    return Redirect($"/Account/MyUserProfile?userId={model.Id}");

                }
                ModelState.AddModelError(nameof(model.CurrentPassword), "Password Incorect");
                return View(model);

            }
            throw new UnauthorizedAccessException();
        }

        [Authorize(Policy = "Artist")]
        [HttpGet]
        public IActionResult MyArtistProfile(int artistId)
        {
            if (artistId == CurrentUser.Id)
            {

                var artist = accountService.GetMyArtistProfileModelById(artistId);
                return View(artist);
            }
            throw new UnauthorizedAccessException();
        }

        [Authorize(Policy = "Artist")]
        [HttpPost]
        public IActionResult MyArtistProfile(MyArtistProfileModel model)
        {
            if (model.Id == CurrentUser.Id )
            {
                if (model.CurrentPassword != null && accountService.IsArtistPasswordCorrect(model.Id, model.CurrentPassword))
                {
                    accountService.UpdateMyArtistProfile(model);
                    Logout();
                    Login(new LoginModel { Email = accountService.GetArtistEmailById(model.Id), Password = model.NewPassword == null ? model.CurrentPassword : model.NewPassword, RememberLogin = false });
                    var x = CurrentUser;
                    return RedirectToAction("MyArtistProfile", "Account", new { artistId = model.Id });
                }
                ModelState.AddModelError(nameof(model.CurrentPassword), "Password Incorect");
                return View(model);
            }
            throw new UnauthorizedAccessException();
        }

        [Authorize(Policy = "Organizer")]
        [HttpGet]
        public IActionResult MyOrganizerProfile(int organizerId)
        {
            if (organizerId == CurrentUser.Id) {
                var organizer = accountService.GetMyOrganizerProfileModelById(organizerId);
                return View(organizer);
            }
            throw new UnauthorizedAccessException();
        }

        [Authorize(Policy = "Organizer")]
        [HttpPost]
        public IActionResult MyOrganizerProfile(MyOrganizerProfileModel model)
        {
            if (model.Id == CurrentUser.Id)
            {
                if (model.CurrentPassword != null  && accountService.IsOrganizerPasswordCorrect(model.Id, model.CurrentPassword))
                {
                    accountService.UpdateOrganizerProfile(model);
                    Logout();
                    Login(new LoginModel { Email = accountService.GetOrganizerEmailById(model.Id), Password = model.NewPassword == null ? model.CurrentPassword : model.NewPassword, RememberLogin = false });
                    return Redirect($"/Account/MyOrganizerProfile?organizerId={model.Id}");
                }
                ModelState.AddModelError(nameof(model.CurrentPassword), "Password Incorect");
                return View(model);
            }
            throw new UnauthorizedAccessException();
        }

        public IActionResult ShowOrganizer(int organizerId)
        {
            var organizer = accountService.ShowOrganizer(organizerId);
            return View(organizer);
        }

        [Authorize(Policy = "User")]
        public IActionResult MyFavourites()
        {
            var favourites = accountService.GetMyFavourites(CurrentUser.Id);
            return View(favourites);
        }
    }
}
