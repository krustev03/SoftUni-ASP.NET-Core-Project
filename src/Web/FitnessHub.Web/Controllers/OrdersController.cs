namespace FitnessHub.Web.Controllers
{
    using FitnessHub.Web.ViewModels.Order;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        public IActionResult CardDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CardDetails(CreditCardInputValidationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.Redirect("/Orders/Buy");
        }

        public IActionResult Buy()
        {
            return View();
        }
    }
}
