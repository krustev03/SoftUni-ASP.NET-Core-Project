namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.MyCart;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MyCartController : Controller
    {
        private readonly IMyCartService myCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public MyCartController(IMyCartService myCartService, UserManager<ApplicationUser> userManager)
        {
            this.myCartService = myCartService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var appUser = await this.userManager.GetUserAsync(this.User);

            var viewModel = new CartItemsViewModel
            {
                Equipments = this.myCartService.GetUserEquipments(appUser),
                Suplements = this.myCartService.GetUserSuplements(appUser),
                TotalPrice = this.myCartService.GetTotalPrice(appUser),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEquipmentFromCart(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;

            await this.myCartService.RemoveEquipmentFromCartAsync(id, userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSuplementFromCart(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;

            await this.myCartService.RemoveSuplementFromCartAsync(id, userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult GoToHome()
        {
            return this.Redirect("/Home/Index");
        }
    }
}
