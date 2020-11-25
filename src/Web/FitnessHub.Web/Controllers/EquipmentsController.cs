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
        private readonly IWebHostEnvironment webHostEnvironment;

        public EquipmentsController(
            IEquipmentsService equipmentsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.equipmentsService = equipmentsService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

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

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EquipmentInputModel model)
        {
            if (!model.Image.FileName.EndsWith(".jpg"))
            {
                this.ModelState.AddModelError("Image", "Invalid file type.");
            }

            var allEquipments = this.equipmentsService.GetAllEquipments<EquipmentViewModel>();

            if (allEquipments.Any(x => x.Name == model.Name))
            {
                this.ModelState.AddModelError("Name", $"Name '{model.Name}' has already been taken.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            using (FileStream fs = new FileStream(
                this.webHostEnvironment.WebRootPath + $"/equipmentsImages/{model.Name}.jpg", FileMode.Create))
            {
                await model.Image.CopyToAsync(fs);
            }

            await this.equipmentsService.AddEquipmentAsync(model);
            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        public IActionResult Edit(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int equipmentId, int page, EquipmentInputModel model)
        {
            if (!model.Image.FileName.EndsWith(".jpg"))
            {
                this.ModelState.AddModelError("Image", "Invalid file type.");
            }

            var allEquipments = this.equipmentsService.GetAllEquipments<EquipmentViewModel>().ToList();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            using (FileStream fs = new FileStream(
                this.webHostEnvironment.WebRootPath + $"/equipmentsImages/{model.Name}.jpg", FileMode.Create))
            {
                await model.Image.CopyToAsync(fs);
            }

            await this.equipmentsService.EditEquipment(equipmentId, model);

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int equipmentId, int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.equipmentsService.AddEquipmentToCart(equipmentId, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int equipmentId, int page)
        {
            await this.equipmentsService.DeleteEquipmentByIdAsync(equipmentId);

            return this.RedirectToAction("Index", new { page });
        }
    }
}
