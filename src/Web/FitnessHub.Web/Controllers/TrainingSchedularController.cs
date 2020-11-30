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
        private readonly ITrainingProgramService trainingProgramsService;
        private readonly ITrainingService trainingsService;
        private readonly IMuscleGroupService muscleGroupsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrainingSchedularController(
            ITrainingProgramService trainingProgramsService,
            ITrainingService trainingsService,
            IMuscleGroupService muscleGroupsService,
            UserManager<ApplicationUser> userManager)
        {
            this.trainingProgramsService = trainingProgramsService;
            this.trainingsService = trainingsService;
            this.muscleGroupsService = muscleGroupsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;
            var appUser = await this.userManager.GetUserAsync(this.User);
            string userId = appUser.Id;
            var viewModel = new TrainingProgramsIndexViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                ItemsCount = this.trainingProgramsService.GetCount(),
                TrainingPrograms = this.trainingProgramsService.GetAllForPaging<TrainingProgramViewModel>(page, userId, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AddTrainingProgram()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTrainingProgram(TrainingProgramInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.trainingProgramsService.AddTrainingProgramAsync(model, appUser);
            var page = 1;

            return this.RedirectToAction("Index", new { page });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteProgram(int programId, int page)
        {
            await this.trainingProgramsService.DeleteProgramByIdAsync(programId);

            return this.RedirectToAction("Index", new { page });
        }

        [Authorize]
        public IActionResult ChangeProgramName(int programId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeProgramName(int programId, int page, TrainingProgramInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingProgramsService.ChangeName(programId, model);

            return this.RedirectToAction("AllTrainings", new { programId, page });
        }

        [Authorize]
        public IActionResult AllTrainings(int programId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTraining(string dayOfWeek, int programId, int page)
        {
            await this.trainingsService.AddTrainingAsync(programId, dayOfWeek);

            return this.RedirectToAction("AllTrainings", new { programId,  page });
        }

        [Authorize]
        public IActionResult TrainingDetails(int programId, int trainingId, int page)
        {
            var trainingModel = this.trainingsService.GetTrainingDetails<TrainingDetailsViewModel>(trainingId);

            return this.View(trainingModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteTraining(int programId, int trainingId, int page)
        {
            await this.trainingsService.DeleteTrainingByIdAsync(trainingId);

            return this.RedirectToAction("AllTrainings", new { programId, page });
        }

        [Authorize]
        public IActionResult AddExercise(int programId, int trainingId, int page)
        {
            var viewModel = new ExerciseInputModel
            {
                MuscleGroupsItems = this.muscleGroupsService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExercise(int programId, int trainingId, int page, ExerciseInputModel model)
        {
            await this.trainingsService.AddExerciseToTrainingAsync(trainingId, model);

            return this.RedirectToAction("TrainingDetails", new { programId, trainingId, page });
        }

        [Authorize]
        public IActionResult EditExercise(int programId, int trainingId, int exerciseId, int page)
        {
            var viewModel = new ExerciseInputModel
            {
                MuscleGroupsItems = this.muscleGroupsService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditExercise(int programId, int trainingId, int exerciseId, int page, ExerciseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingsService.EditExercise(exerciseId, model);

            return this.RedirectToAction("TrainingDetails", new { programId, trainingId, page });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteExercise(int programId, int trainingId, int exerciseId, int page)
        {
            await this.trainingsService.DeleteExerciseByIdAsync(exerciseId);

            return this.RedirectToAction("TrainingDetails", new { programId, trainingId, page });
        }

        [Authorize]
        public IActionResult ReturnToProgram(int programId, int page)
        {
            return this.RedirectToAction("AllTrainings", new { programId, page });
        }

        [Authorize]
        public IActionResult ReturnToAllPrograms(int page)
        {
            return this.RedirectToAction("Index", new { page });
        }
    }
}
