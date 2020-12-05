namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Equipments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EquipmentsController : Controller
    {
        private readonly IEquipmentService equipmentService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public EquipmentsController(
            IEquipmentService equipmentService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.equipmentService = equipmentService;
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
                ItemsCount = this.equipmentService.GetCount(),
                Equipments = this.equipmentService.GetAllForPaging<EquipmentViewModel>(page, ItemsPerPage),
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
        public async Task<IActionResult> Add(CreateEquipmentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.equipmentService.AddEquipmentAsync(model, appUser.Id, $"{this.environment.WebRootPath}/images");
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
        public IActionResult Edit(int equipmentId)
        {
            var model = this.equipmentService.GetEquipmentById<EditEquipmentInputModel>(equipmentId);
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int equipmentId, int page, EditEquipmentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.equipmentService.EditEquipment(model, equipmentId, appUser.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int equipmentId, int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.equipmentService.AddEquipmentToCart(equipmentId, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int equipmentId, int page)
        {
            await this.equipmentService.DeleteEquipmentByIdAsync(equipmentId);

            return this.RedirectToAction("Index", new { page });
        }
    }
}
