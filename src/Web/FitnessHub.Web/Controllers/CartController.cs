namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(
            ICartService cartService,
            UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appUser = await this.userManager.GetUserAsync(this.User);

            var viewModel = new CartItemsViewModel
            {
                Equipments = this.cartService.GetUserEquipments(appUser.Id),
                Suplements = this.cartService.GetUserSuplements(appUser.Id),
                TotalPrice = this.cartService.GetTotalPrice(appUser.Id),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveEquipmentFromCart(int equipmentId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;

            await this.cartService.RemoveEquipmentFromCartAsync(equipmentId, userId);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveSuplementFromCart(int suplementId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;

            await this.cartService.RemoveSuplementFromCartAsync(suplementId, userId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
