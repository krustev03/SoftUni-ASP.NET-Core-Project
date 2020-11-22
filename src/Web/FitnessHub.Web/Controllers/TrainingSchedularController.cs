namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TrainingSchedularController : Controller
    {
        private readonly ITrainingProgramsService trainingProgramsService;
        private readonly ITrainingsService trainingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrainingSchedularController(
            ITrainingProgramsService trainingProgramsService,
            ITrainingsService trainingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.trainingProgramsService = trainingProgramsService;
            this.trainingsService = trainingsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult AddTrainingProgram()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingProgram(AddTrainingProgramInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.trainingProgramsService.AddTrainingProgramAsync(model, appUser);

            return this.RedirectToAction("Index");
        }

        public IActionResult AllTrainings(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTraining(string dayOfWeek, int programId)
        {
            await this.trainingsService.AddTrainingAsync(programId, dayOfWeek);

            return this.RedirectToAction("Index");
        }

        public IActionResult TrainingDetails(int trainingId)
        {
            var trainingModel = this.trainingsService.GetTrainingDetails<TrainingDetailsViewModel>(trainingId);

            return this.View(trainingModel);
        }

        public IActionResult AddExercise(int trainingId)
        {
            return this.View();
        }
    }
}
