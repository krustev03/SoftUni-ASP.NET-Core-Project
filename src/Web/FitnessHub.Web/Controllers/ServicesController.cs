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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 8;
            var viewModel = new ServicesIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.servicesService.GetCount(),
                Services = this.servicesService.GetAllForPaging<ServiceViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ServiceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.servicesService.AddServiceAsync(model, appUser);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int serviceId, int page, ServiceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.servicesService.EditService(serviceId, model);

            return this.Redirect($"Details?serviceId={serviceId}&&page={page}");
        }

        [Authorize]
        public IActionResult Details(int serviceId, int page)
        {
            var serviceModel = this.servicesService.GetServiceDetails<ServiceDetailsViewModel>(serviceId);

            return this.View(serviceModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int serviceId, int page)
        {
            await this.servicesService.DeleteServiceByIdAsync(serviceId);

            return this.Redirect($"/Services/All/{page}");
        }

        [Authorize]
        public IActionResult Return(int page)
        {
            return this.Redirect($"/Services/All/{page}");
        }
    }
}
