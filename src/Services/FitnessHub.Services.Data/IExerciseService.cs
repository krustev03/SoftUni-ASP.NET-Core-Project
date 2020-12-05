namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface IExerciseService
    {
        Task AddExerciseToTrainingAsync(int trainingId, ExerciseInputModel model);

        Task EditExercise(int exerciseId, ExerciseInputModel model);

        T GetExerciseById<T>(int exerciseId);

        Task DeleteExerciseByIdAsync(int exerciseId);
    }
}
