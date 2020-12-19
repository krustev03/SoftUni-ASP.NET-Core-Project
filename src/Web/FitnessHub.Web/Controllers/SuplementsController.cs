namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class SuplementsController : Controller
    {
        private readonly ISuplementService suplementService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public SuplementsController(
            ISuplementService suplementService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.suplementService = suplementService;
            this.userManager = userManager;
            this.environment = environment;
        }

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
                ItemsCount = this.suplementService.GetCount(),
                Suplements = this.suplementService.GetAllForPaging<SuplementViewModel>(page, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult FilteredIndex(int page, string searchString, SuplementsIndexViewModel model)
        {
            if (model.SearchString == null)
            {
                model.SearchString = searchString;
            }

            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;
            var viewModel = new SuplementsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.suplementService.GetFilteredCount(model.SearchString),
                Suplements = this.suplementService.GetAllWithFilterForPaging<SuplementViewModel>(page, model.SearchString, ItemsPerPage),
                IsFiltered = true,
                SearchString = model.SearchString,
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
        public async Task<IActionResult> Add(CreateSuplementInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.suplementService.AddSuplementAsync(model, appUser.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            var page = 1;

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int suplementId)
        {
            var model = this.suplementService.GetSuplementById<EditSuplementInputModel>(suplementId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int suplementId, int page, EditSuplementInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.suplementService.EditSuplement(model, suplementId, appUser.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int suplementId, int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.suplementService.AddSuplementToCart(suplementId, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int suplementId, int page)
        {
            await this.suplementService.DeleteSuplementByIdAsync(suplementId);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }
    }
}
