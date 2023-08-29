using MapMusic.Common.DTOs;

namespace MapMusic.WebApp.Code
{
    public class ControllerDependencies
    {
        public CurrentUserDTO CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDTO currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
