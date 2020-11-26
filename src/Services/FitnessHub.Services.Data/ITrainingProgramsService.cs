namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface ITrainingProgramsService
    {
        public Task AddTrainingProgramAsync(AddTrainingProgramInputModel programModel, ApplicationUser appUser);

        public Task ChangeName(int id, AddTrainingProgramInputModel model);

        public IEnumerable<T> GetAllForPaging<T>(int page, string userId, int itemsPerPage);

        public int GetCount();

        public Task DeleteProgramByIdAsync(int id);
    }
}