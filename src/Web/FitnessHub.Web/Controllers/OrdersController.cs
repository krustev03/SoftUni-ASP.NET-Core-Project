namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Services.Messaging;
    using FitnessHub.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMailService mailService;
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public OrdersController(
            IMailService mailService,
            UserManager<ApplicationUser> userManager,
            IOrderService orderService,
            IConfiguration configuration)
        {
            this.mailService = mailService;
            this.userManager = userManager;
            this.orderService = orderService;
            this.configuration = configuration;
        }

        public IActionResult CardDetails(decimal totalPrice)
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CardDetails(string totalPrice, CardValidationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.UserDetails), new { totalPrice });
        }

        public IActionResult StripePaymentDetails(decimal totalPrice)
        {
            this.ViewBag.StripePublishKey = this.configuration["Stripe:PublishableKey"];
            return this.View();
        }

        [HttpPost]
        public IActionResult StripePaymentDetails(string totalPrice, string stripeToken, string stripeEmail)
        {
            var price = Convert.ToDecimal(totalPrice);
            var charge = new Stripe.ChargeCreateOptions();
            charge.Amount = Convert.ToInt64(price) * 100;
            charge.Currency = "BGN";
            charge.ReceiptEmail = stripeEmail;
            charge.Description = "Sample Charge";
            charge.Source = stripeToken;
            charge.Capture = true;
            var chargeService = new Stripe.ChargeService();
            Charge stripeCharge = chargeService.Create(charge);

            return this.RedirectToAction(nameof(this.UserDetails), new { totalPrice });
        }

        public IActionResult UserDetails(decimal totalPrice)
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

            await this.orderService.AddOrderAsync(model, appUser.Id);

            await this.mailService.SendEmailAsync(appUser);
            await this.userManager.UpdateAsync(appUser);

            return this.RedirectToAction(nameof(this.ThankYou));
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
