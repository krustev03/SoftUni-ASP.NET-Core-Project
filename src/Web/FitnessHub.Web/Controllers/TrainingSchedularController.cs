namespace FitnessHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TrainingSchedularController : Controller
    {
        private readonly ITrainingProgramService trainingProgramService;
        private readonly ITrainingService trainingService;
        private readonly IExerciseService exerciseService;
        private readonly IMuscleGroupService muscleGroupService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrainingSchedularController(
            ITrainingProgramService trainingProgramService,
            ITrainingService trainingService,
            IExerciseService exerciseService,
            IMuscleGroupService muscleGroupService,
            UserManager<ApplicationUser> userManager)
        {
            this.trainingProgramService = trainingProgramService;
            this.trainingService = trainingService;
            this.exerciseService = exerciseService;
            this.muscleGroupService = muscleGroupService;
            this.userManager = userManager;
        }

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
                ItemsCount = this.trainingProgramService.GetCount(userId),
                TrainingPrograms = this.trainingProgramService.GetAllForPaging<TrainingProgramViewModel>(page, userId, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult AddTrainingProgram()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingProgram(TrainingProgramInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appUser = await this.userManager.GetUserAsync(this.User);

            await this.trainingProgramService.AddTrainingProgramAsync(model, appUser.Id);
            var page = 1;

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProgram(int programId, int page)
        {
            await this.trainingProgramService.DeleteProgramByIdAsync(programId);

            return this.RedirectToAction(nameof(this.Index), new { page });
        }

        public IActionResult ChangeProgramName(int programId)
        {
            var model = this.trainingProgramService.GetProgramById<TrainingProgramInputModel>(programId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProgramName(int programId, int page, TrainingProgramInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingProgramService.ChangeName(programId, model);

            return this.RedirectToAction(nameof(this.AllTrainings), new { programId, page });
        }

        public IActionResult AllTrainings(int programId)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTraining(string dayOfWeek, int programId, int page)
        {
            await this.trainingService.AddTrainingAsync(programId, dayOfWeek);

            return this.RedirectToAction(nameof(this.AllTrainings), new { programId,  page });
        }

        public IActionResult TrainingDetails(int programId, int trainingId, int page)
        {
            var trainingModel = this.trainingService.GetTraining<TrainingDetailsViewModel>(trainingId);

            return this.View(trainingModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTraining(int programId, int trainingId, int page)
        {
            await this.trainingService.DeleteTrainingByIdAsync(trainingId, programId);

            return this.RedirectToAction(nameof(this.AllTrainings), new { programId, page });
        }

        public IActionResult AddExercise(int programId, int trainingId, int page)
        {
            var viewModel = new ExerciseInputModel
            {
                MuscleGroupsItems = this.muscleGroupService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise(int programId, int trainingId, int page, ExerciseInputModel model)
        {
            await this.exerciseService.AddExerciseToTrainingAsync(trainingId, model);

            return this.RedirectToAction(nameof(this.TrainingDetails), new { programId, trainingId, page });
        }

        public IActionResult EditExercise(int programId, int trainingId, int exerciseId, int page)
        {
            var model = this.exerciseService.GetExerciseById<ExerciseInputModel>(exerciseId);
            model.MuscleGroupsItems = this.muscleGroupService.GetAllAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditExercise(int programId, int trainingId, int exerciseId, int page, ExerciseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.exerciseService.EditExercise(exerciseId, model);

            return this.RedirectToAction(nameof(this.TrainingDetails), new { programId, trainingId, page });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExercise(int programId, int trainingId, int exerciseId, int page)
        {
            await this.exerciseService.DeleteExerciseByIdAsync(exerciseId);

            return this.RedirectToAction(nameof(this.TrainingDetails), new { programId, trainingId, page });
        }

        public IActionResult ReturnToProgram(int programId, int page)
        {
            return this.RedirectToAction(nameof(this.AllTrainings), new { programId, page });
        }

        public IActionResult ReturnToAllPrograms(int page)
        {
            return this.RedirectToAction(nameof(this.Index), new { page });
        }
    }
}
