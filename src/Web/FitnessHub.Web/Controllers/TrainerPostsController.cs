﻿namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.TrainerPosts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TrainerPostsController : Controller
    {
        private readonly ITrainerPostsService trainerPostsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrainerPostsController(ITrainerPostsService trainerPostsService, UserManager<ApplicationUser> userManager)
        {
            this.trainerPostsService = trainerPostsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;
            var viewModel = new TrainerPostsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.trainerPostsService.GetCount(),
                TrainerPosts = this.trainerPostsService.GetAllForPaging<TrainerPostViewModel>(page, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator, Trainer")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Trainer")]
        public async Task<IActionResult> Add(TrainerPostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.trainerPostsService.AddPostAsync(model, appUser);

            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        [Authorize(Roles = "Administrator, Trainer")]
        public IActionResult Edit(int trainerPostId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Trainer")]
        public async Task<IActionResult> Edit(int trainerPostId, int page, TrainerPostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainerPostsService.EditPost(trainerPostId, model);

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Trainer")]
        public async Task<IActionResult> Delete(int trainerPostId, int page)
        {
            await this.trainerPostsService.DeletePostByIdAsync(trainerPostId);

            return this.RedirectToAction("Index", new { page });
        }
    }
}