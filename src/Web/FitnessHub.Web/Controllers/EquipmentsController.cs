namespace FitnessHub.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Equipments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EquipmentsController : BaseController
    {
        private readonly IEquipmentsService equipmentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public EquipmentsController(
            IEquipmentsService equipmentsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.equipmentsService = equipmentsService;
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
            var viewModel = new EquipmentsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.equipmentsService.GetCount(),
                Equipments = this.equipmentsService.GetAllForPaging<EquipmentViewModel>(page, ItemsPerPage),
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
        public async Task<IActionResult> Add(EquipmentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.equipmentsService.AddEquipmentAsync(model, appUser.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int equipmentId, int page, EquipmentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.equipmentsService.EditEquipment(model, equipmentId, appUser.Id, $"{this.environment.WebRootPath}/images");
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
        public async Task<IActionResult> Delete(int equipmentId, int page)
        {
            await this.equipmentsService.DeleteEquipmentByIdAsync(equipmentId);

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int equipmentId, int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.equipmentsService.AddEquipmentToCart(equipmentId, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }
    }
}