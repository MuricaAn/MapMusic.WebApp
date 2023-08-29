using MapMusic.BusinessLogic.Base;
using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.BusinessLogic.Implementation.Event;
using MapMusic.BusinessLogic.Implementation.Location;
using MapMusic.BusinessLogic.Implementation.Organizer;
using MapMusic.BusinessLogic.Implementation.Organizer.Mappings;
using MapMusic.BusinessLogic.Implementation.PhotoImp;
using MapMusic.BusinessLogic.Implementation.Rating;
using MapMusic.BusinessLogic.Implementation.VwSearchBar;
using MapMusic.Common.DTOs;
using MapMusic.Entities.Enums;
using System.Security.Claims;

namespace MapMusic.WebApp.Code.ExtensionMethods
{

    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<AccountService>();
            services.AddScoped<OrganizerService>();
            services.AddScoped<EventService>();
            services.AddScoped<LocationService>();
            services.AddScoped<PhotoService>();
            services.AddScoped<RatingService>();
            services.AddScoped<VwSearBarEntitiesService>();
            return services;
        }

        public static IServiceCollection AddCurrentUser (this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor?.HttpContext;
                if (httpContext == null || !httpContext!.User!.Identity!.IsAuthenticated)
                    return new CurrentUserDTO { IsLoggedIn = false };
                var isIdValid = int.TryParse(httpContext.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value, out int id);
                if (!isIdValid)
                {
                    throw new Exception("Id-ul nu e int");
                }
                var isRoleIdValid = int.TryParse(httpContext.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Role)?.Value, out int roleId);
                if (!isRoleIdValid)
                {
                    throw new Exception("IdRole-ul nu e int");
                }
                return new CurrentUserDTO
                {
                    Id = id,
                    FullName = httpContext.User?.Claims?.FirstOrDefault(s => s.Type == ClaimTypes.Name)?.Value ?? "",
                    RoleId = roleId,
                    IsLoggedIn = true
                };
            });
            return services;
        }

        public static IServiceCollection AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole(((int)RoleType.Admin).ToString()));
                options.AddPolicy("Organizer", policy =>
                    policy.RequireRole(((int)RoleType.Organizer).ToString()));
                options.AddPolicy("Artist", policy =>
                    policy.RequireRole(((int)RoleType.Artist).ToString()));
                options.AddPolicy("User", policy =>
                    policy.RequireRole(((int)RoleType.User).ToString()));
            });
            return services;
        }

        //public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        //{
        //    services.AddScoped<ToOrganizerRequestShow>();
        //    return services;
        //}
    }
}
