namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IMailService mailService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(
            IMailService mailService, 
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService)
        {
            this.mailService = mailService;
            this.userManager = userManager;
            this.ordersService = ordersService;
        }

        public IActionResult CardDetails()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CardDetails(CreditCardInputValidationModel model)
        {
            string price = this.HttpContext.Request.Query["price"];

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction("UserDetails", new { price });
        }

        public IActionResult UserDetails()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UserDetails(OrderInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.ordersService.AddOrderAsync(model, appUser);
            await this.mailService.SendEmailAsync(appUser);

            return this.Redirect("/Orders/ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
