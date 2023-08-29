using MapMusic.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MapMusic.WebApp.Code
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDTO CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUser = dependencies.CurrentUser;
        }
    }
}
