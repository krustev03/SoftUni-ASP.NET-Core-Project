namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface IExerciseService
    {
        Task AddExerciseToTrainingAsync(int trainingId, ExerciseInputModel model);

        Task DeleteExerciseByIdAsync(int exerciseId);

        Task EditExercise(int exerciseId, ExerciseInputModel model);
    }
}