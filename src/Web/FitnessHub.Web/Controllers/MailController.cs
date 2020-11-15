namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Mail;
    using Hangfire;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MailController : Controller
    {
        private readonly IMailService mailService;
        private readonly UserManager<ApplicationUser> userManager;

        public MailController(IMailService mailService, UserManager<ApplicationUser> userManager)
        {
            this.mailService = mailService;
            this.userManager = userManager;
        }

        public IActionResult SendMail()
        {
            return this.View();
        }

        public async Task<IActionResult> SendEmail()
        {
            return this.View();
        }
    }
}
