namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ServicesController : BaseController
    {
        private readonly IServicesService servicesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ServicesController(IServicesService servicesService, UserManager<ApplicationUser> userManager)
        {
            this.servicesService = servicesService;
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
            var viewModel = new ServicesIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.servicesService.GetCount(),
                Services = this.servicesService.GetAllForPaging<ServiceViewModel>(page, ItemsPerPage),
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
        public async Task<IActionResult> Add(ServiceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.servicesService.AddServiceAsync(model, appUser);

            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int serviceId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int serviceId, int page, ServiceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.servicesService.EditService(serviceId, model);

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int serviceId, int page)
        {
            await this.servicesService.DeleteServiceByIdAsync(serviceId);

            return this.RedirectToAction("Index", new { page });
        }
    }
}
