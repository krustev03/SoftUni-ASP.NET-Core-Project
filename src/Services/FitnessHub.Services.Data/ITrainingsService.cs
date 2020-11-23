namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface ITrainingsService
    {
        public Task AddTrainingAsync(int programId, string dayOfWeek);

        public T GetTrainingDetails<T>(int id);

        public Task DeleteTrainingByIdAsync(int trainingId);

        public Task AddExerciseToTrainingAsync(int trainingId, AddExerciseInputModel model);

        public Task EditExercise(int id, AddExerciseInputModel model);

        public Task DeleteExerciseByIdAsync(int exerciseId);
    }
}