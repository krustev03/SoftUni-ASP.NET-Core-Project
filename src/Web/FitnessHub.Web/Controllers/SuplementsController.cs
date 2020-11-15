namespace FitnessHub.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Internal;

    public class SuplementsController : BaseController
    {
        private readonly ISuplementsService suplementsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SuplementsController(
            ISuplementsService suplementsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.suplementsService = suplementsService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult All()
        {
            var viewModel = new SuplementsIndexViewModel();
            viewModel.Suplements = this.suplementsService.GetAllSuplements<SuplementViewModel>();

            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSuplementInputModel model)
        {
            if (!model.Image.FileName.EndsWith(".jpg"))
            {
                this.ModelState.AddModelError("Image", "Invalid file type.");
            }

            var allSuplements = this.suplementsService.GetAllSuplements<SuplementViewModel>().ToList();

            if (allSuplements.Any(x => x.Name == model.Name))
            {
                this.ModelState.AddModelError("Name", $"Name '{model.Name}' has already been taken.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            using (FileStream fs = new FileStream(
                this.webHostEnvironment.WebRootPath + $"/suplementsImages/{model.Name}.jpg", FileMode.Create))
            {
                await model.Image.CopyToAsync(fs);
            }

            await this.suplementsService.AddSuplementAsync(model);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Details()
        {
            var url = this.HttpContext.Request.Path.Value;
            var id = Convert.ToInt32(url.Substring(url.LastIndexOf('/') + 1));
            var suplementModel = this.suplementsService.GetSuplementDetails<SuplementDetailsViewModel>(id);

            return this.View(suplementModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user.Id;
            await this.suplementsService.AddSuplementToCart(id, userId);
            await this.userManager.UpdateAsync(user);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.suplementsService.DeleteSuplementByIdAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Return()
        {
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
