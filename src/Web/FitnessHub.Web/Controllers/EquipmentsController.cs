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

        public IActionResult All()
        {
            var viewModel = new EquipmentsIndexViewModel();
            viewModel.Equipments = this.equipmentsService.GetAllEquipments<EquipmentViewModel>();

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

            var allEquipments = this.equipmentsService.GetAllEquipments<EquipmentViewModel>().ToList();

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
        public async Task<IActionResult> Edit(int id, EquipmentInputModel model)
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

            await this.equipmentsService.EditEquipment(id, model);

            return this.RedirectToAction("Details", new { id });
        }

        public IActionResult Details()
        {
            var url = this.HttpContext.Request.Path.Value;
            var id = Convert.ToInt32(url.Substring(url.LastIndexOf('/') + 1));
            var equipmentModel = this.equipmentsService.GetEquipmentDetails<EquipmentDetailsViewModel>(id);

            return this.View(equipmentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.equipmentsService.DeleteEquipmentByIdAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.equipmentsService.AddEquipmentToCart(id, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Return()
        {
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
