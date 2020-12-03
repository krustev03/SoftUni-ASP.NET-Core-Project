namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    public interface ITrainingService
    {
        Task AddTrainingAsync(int programId, string dayOfWeek);

        T GetTraining<T>(int trainingId);

        Task DeleteTrainingByIdAsync(int trainingId, int programId);
    }
}
