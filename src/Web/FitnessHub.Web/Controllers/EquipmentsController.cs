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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 4;
            var viewModel = new EquipmentsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.equipmentsService.GetCount(),
                Equipments = this.equipmentsService.GetAllForPaging<EquipmentViewModel>(id, ItemsPerPage),
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

            return this.RedirectToAction(nameof(this.All));
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

            return this.Redirect($"Details?equipmentId={equipmentId}&&page={page}");
        }

        public IActionResult Details(int equipmentId, int page)
        {
            var equipmentModel = this.equipmentsService.GetEquipmentDetails<EquipmentDetailsViewModel>(equipmentId);

            return this.View(equipmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int equipmentId, int page)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.equipmentsService.AddEquipmentToCart(equipmentId, userId);
            await this.userManager.UpdateAsync(user);

            return this.Redirect($"/Equipments/All/{page}");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int equipmentId, int page)
        {
            await this.equipmentsService.DeleteEquipmentByIdAsync(equipmentId);

            return this.Redirect($"/Equipments/All/{page}");
        }

        public IActionResult Return(int page)
        {
            return this.Redirect($"/Equipments/All/{page}");
        }
    }
}
