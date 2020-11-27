namespace FitnessHub.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IMailService mailService;
        private readonly IEmailSender emailSender;
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(
            IMailService mailService,
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService,
            IEmailSender emailSender)
        {
            this.mailService = mailService;
            this.userManager = userManager;
            this.ordersService = ordersService;
            this.emailSender = emailSender;
        }

        [Authorize]
        public IActionResult CardDetails(decimal price)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CardDetails(decimal price, CreditCardInputValidationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction("UserDetails", new { price });
        }

        public IActionResult UserDetails(decimal price)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserDetails(OrderInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.ordersService.AddOrderAsync(model, appUser.Id);
            await this.mailService.SendEmailAsync(appUser);
            await this.userManager.UpdateAsync(appUser);

            return this.RedirectToAction("ThankYou");
        }

        [Authorize]
        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
