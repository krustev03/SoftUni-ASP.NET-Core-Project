namespace FitnessHub.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult GetPartial()
        {
            return this.PartialView("_ViewAll");
        }
    }
}
