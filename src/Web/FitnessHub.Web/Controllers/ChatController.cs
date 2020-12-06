namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ChatController : Controller
    {
        private readonly IMessageService messageService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatController(IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            this.messageService = messageService;
            this.userManager = userManager;
        }

        [Authorize]
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
