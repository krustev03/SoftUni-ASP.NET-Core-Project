﻿namespace FitnessHub.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SuplementsController : BaseController
    {
        private readonly ISuplementService suplementsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public SuplementsController(
            ISuplementService suplementsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.suplementsService = suplementsService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;
            var viewModel = new SuplementsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.suplementsService.GetCount(),
                Suplements = this.suplementsService.GetAllForPaging<SuplementViewModel>(page, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(SuplementInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.suplementsService.AddSuplementAsync(model, appUser.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        public IActionResult Edit(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int suplementId, int page, SuplementInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.suplementsService.EditSuplement(model, suplementId, appUser.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int suplementId, int page)
        {
            await this.suplementsService.DeleteSuplementByIdAsync(suplementId);

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int suplementId, int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.suplementsService.AddSuplementToCart(suplementId, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }
    }
}