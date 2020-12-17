namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface ITrainingProgramService
    {
        Task AddTrainingProgramAsync(TrainingProgramInputModel programModel, string userId);

        Task ChangeName(int programId, TrainingProgramInputModel model);

        IEnumerable<T> GetAllForPaging<T>(int page, string userId, int itemsPerPage);

        T GetProgramById<T>(int programId);

        int GetCount(string userId);

        Task DeleteProgramByIdAsync(int programId);
    }
}
