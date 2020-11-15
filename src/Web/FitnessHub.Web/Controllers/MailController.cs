namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Mail;
    using Microsoft.AspNetCore.Mvc;

    public class MailController : Controller
    {
        private readonly IMailService mailService;

        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        public IActionResult SendMail()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(MailRequest request)
        {
            try
            {
                await this.mailService.SendEmailAsync(request);
                return this.Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
