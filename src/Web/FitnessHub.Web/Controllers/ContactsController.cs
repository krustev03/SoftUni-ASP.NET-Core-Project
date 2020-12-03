namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static FitnessHub.Common.GlobalConstants;

    public class ContactsController : Controller
    {
        private readonly IEmailSender emailSender;

        public ContactsController(
            IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(ContactsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var email = model.Email;
            var name = model.Name;
            var subject = model.Subject;
            var content = model.Message;

            await this.emailSender.SendPlainEmailAsync(email, name, SupportEmail, subject, content, null);

            return this.RedirectToAction("ThankYou");
        }

        [Authorize]
        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
