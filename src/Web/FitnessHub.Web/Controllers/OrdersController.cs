namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Configuration;
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

        [Authorize]
        public IActionResult CardDetails(decimal totalPrice)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CardDetails(string totalPrice, CardValidationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction($"UserDetails", new { totalPrice });
        }

        [Authorize]
        public IActionResult StripePaymentDetails(decimal totalPrice)
        {
            this.ViewBag.StripePublishKey = this.configuration["Stripe:PublishableKey"];
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult StripePaymentDetails(string totalPrice, string stripeToken, string stripeEmail)
        {
            var price = Convert.ToDecimal(totalPrice);
            var myCharge = new Stripe.ChargeCreateOptions();
            myCharge.Amount = Convert.ToInt64(price) * 100;
            myCharge.Currency = "BGN";
            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = "Sample Charge";
            myCharge.Source = stripeToken;
            myCharge.Capture = true;
            var chargeService = new Stripe.ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);

            return this.RedirectToAction($"UserDetails", new { totalPrice });
        }

        [Authorize]
        public IActionResult UserDetails(decimal totalPrice)
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

            await this.orderService.AddOrderAsync(model, appUser.Id);

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
