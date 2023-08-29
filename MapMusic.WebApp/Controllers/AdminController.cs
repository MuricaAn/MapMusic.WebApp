using MapMusic.BusinessLogic.Implementation.Account;
using MapMusic.BusinessLogic.Implementation.Account.Validations;
using MapMusic.BusinessLogic.Implementation.Organizer;
using MapMusic.WebApp.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MapMusic.WebApp.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : BaseController
    {
        private readonly OrganizerService organizerService;
        public AdminController(ControllerDependencies dependencies, OrganizerService organizerService) : base(dependencies)
        {
            this.organizerService = organizerService;
        }

        public IActionResult ShowCreateAccountOrganizerRequests()
        {
            var organizerRequestShow = organizerService.GetOrganizerRequests();
            return View(organizerRequestShow);
        }
        [HttpGet("/GetAccountOrganizerRequests")]
        public IActionResult GetAccountOrganizerRequests()
        {
            var organizerRequestShow = organizerService.GetOrganizerRequests();
            return Ok(organizerRequestShow);
        }
        [HttpPost("/Admin/AcceptOrganizerRequest")]
        public IActionResult AcceptOrganizerRequest(int organizerRequestId)
        {
            organizerService.AcceptOrganizerRequest(organizerRequestId);
            return Redirect("/Admin/ShowCreateAccountOrganizerRequests");
        }
        [HttpPost]
        public IActionResult RejectOrganizerRequest(int organizerRequestId)
        {
            organizerService.RejectOrganizerRequest(organizerRequestId);
            return Redirect("/Admin/ShowCreateAccountOrganizerRequests");
        }
    }
}
