namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.MyCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MyCartController : Controller
    {
        private readonly IMyCartService myCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public MyCartController(
            IMyCartService myCartService,
            UserManager<ApplicationUser> userManager)
        {
            this.myCartService = myCartService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appUser = await this.userManager.GetUserAsync(this.User);

            var viewModel = new CartItemsViewModel
            {
                Equipments = this.myCartService.GetUserEquipments(appUser.Id),
                Suplements = this.myCartService.GetUserSuplements(appUser.Id),
                TotalPrice = this.myCartService.GetTotalPrice(appUser.Id),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveEquipmentFromCart(int equipmentId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;

            await this.myCartService.RemoveEquipmentFromCartAsync(equipmentId, userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveSuplementFromCart(int suplementId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;

            await this.myCartService.RemoveSuplementFromCartAsync(suplementId, userId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
