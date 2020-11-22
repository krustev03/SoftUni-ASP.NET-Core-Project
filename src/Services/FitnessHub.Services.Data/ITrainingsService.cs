namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;

    public interface ITrainingsService
    {
        public Task AddTrainingAsync(int programId, string dayOfWeek);

        public T GetTrainingDetails<T>(int id);
    }
}