namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ServicesController : BaseController
    {
        private readonly IServicesService servicesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ServicesController(
            IServicesService servicesService,
            UserManager<ApplicationUser> userManager)
        {
            this.servicesService = servicesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var viewModel = new SuplementsIndexViewModel();
            viewModel.Services = this.servicesService.GetAllServices<SuplementViewModel>();

            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSuplementInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.servicesService.AddServiceAsync(model, appUser.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Details()
        {
            var url = this.HttpContext.Request.Path.Value;
            var id = Convert.ToInt32(url.Substring(url.LastIndexOf('/') + 1));
            var serviceModel = this.servicesService.GetServiceDetails<SuplementDetailsViewModel>(id);

            return this.View(serviceModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.servicesService.DeleteServiceByIdAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
