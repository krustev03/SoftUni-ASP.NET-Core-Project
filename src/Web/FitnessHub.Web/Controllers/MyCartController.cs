﻿namespace FitnessHub.Web.Controllers
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

            var viewModel = new CartItemsViewModel();
            viewModel.Equipments = this.myCartService.GetUserEquipments(appUser);
            viewModel.Suplements = this.myCartService.GetUserSuplements(appUser);

            return this.View(viewModel);
        }

        public IActionResult GoToHome()
        {
            return this.Redirect("/Home/Index");
        }
    }
}
