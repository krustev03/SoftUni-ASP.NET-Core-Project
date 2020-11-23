namespace FitnessHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TrainingSchedularController : Controller
    {
        private readonly ITrainingProgramsService trainingProgramsService;
        private readonly ITrainingsService trainingsService;
        private readonly IMuscleGroupsService muscleGroupsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrainingSchedularController(
            ITrainingProgramsService trainingProgramsService,
            ITrainingsService trainingsService,
            IMuscleGroupsService muscleGroupsService,
            UserManager<ApplicationUser> userManager)
        {
            this.trainingProgramsService = trainingProgramsService;
            this.trainingsService = trainingsService;
            this.muscleGroupsService = muscleGroupsService;
            this.userManager = userManager;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteProgram(int programId)
        {
            await this.trainingProgramsService.DeleteProgramByIdAsync(programId);

            return this.Redirect($"Index");
        }

        [Authorize]
        public IActionResult ChangeProgramName(int programId)
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeProgramName(int programId, AddTrainingProgramInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingProgramsService.ChangeName(programId, model);

            return this.Redirect($"AllTrainings?programId={programId}");
        }

        public IActionResult AllTrainings(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTraining(string dayOfWeek, int programId)
        {
            await this.trainingsService.AddTrainingAsync(programId, dayOfWeek);

            return this.Redirect($"AllTrainings?programId={programId}");
        }

        public IActionResult TrainingDetails(int programId, int trainingId)
        {
            var trainingModel = this.trainingsService.GetTrainingDetails<TrainingDetailsViewModel>(trainingId);

            return this.View(trainingModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTraining(int programId, int trainingId)
        {
            await this.trainingsService.DeleteTrainingByIdAsync(trainingId);

            return this.Redirect($"AllTrainings?programId={programId}");
        }

        public IActionResult AddExercise(int programId, int trainingId)
        {
            var viewModel = new AddExerciseInputModel();
            viewModel.MuscleGroupsItems = this.muscleGroupsService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise(int programId, int trainingId, AddExerciseInputModel model)
        {
            await this.trainingsService.AddExerciseToTrainingAsync(trainingId, model);

            return this.Redirect($"TrainingDetails?programId={programId}&&trainingId={trainingId}");
        }

        [Authorize]
        public IActionResult EditExercise(int programId, int trainingId, int exerciseId)
        {
            var viewModel = new AddExerciseInputModel();
            viewModel.MuscleGroupsItems = this.muscleGroupsService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditExercise(int programId, int trainingId, int exerciseId, AddExerciseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingsService.EditExercise(exerciseId, model);

            return this.Redirect($"TrainingDetails?programId={programId}&&trainingId={trainingId}");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteExercise(int programId, int trainingId, int exerciseId)
        {
            await this.trainingsService.DeleteExerciseByIdAsync(exerciseId);

            return this.Redirect($"TrainingDetails?programId={programId}&&trainingId={trainingId}");
        }

        public IActionResult ReturnToProgram(int programId)
        {
            return this.Redirect($"AllTrainings?programId={programId}");
        }

        public IActionResult ReturnToAllPrograms()
        {
            return this.Redirect($"Index");
        }
    }
}
