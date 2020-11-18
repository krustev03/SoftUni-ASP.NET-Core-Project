﻿namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ChatController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatController(IMessagesService messagesService, UserManager<ApplicationUser> userManager)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddMessageInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.messagesService.AddMessageAsync(model, appUser.Id);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.messagesService.DeleteMessageByIdAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
