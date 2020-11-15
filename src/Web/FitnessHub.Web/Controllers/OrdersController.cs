namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Order;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IMailService mailService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IMailService mailService, UserManager<ApplicationUser> userManager)
        {
            this.mailService = mailService;
            this.userManager = userManager;
        }

        public IActionResult CardDetails()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CardDetails(CreditCardInputValidationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.Redirect("/Orders/ThankYou");
        }

        public IActionResult Buy()
        {
            return this.View();
        }

        public async Task<IActionResult> ThankYou()
        {
            try
            {
                var appUser = await this.userManager.GetUserAsync(this.User);
                await this.mailService.SendEmailAsync(appUser);
                return this.View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
