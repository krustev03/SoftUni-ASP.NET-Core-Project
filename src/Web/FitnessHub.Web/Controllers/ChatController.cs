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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(MessageInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.messageService.AddMessageAsync(model, appUser.Id);

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int messageId)
        {
            await this.messageService.DeleteMessageByIdAsync(messageId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
