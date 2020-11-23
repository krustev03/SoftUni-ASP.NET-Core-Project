namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface ITrainingProgramsService
    {
        public Task AddTrainingProgramAsync(AddTrainingProgramInputModel programModel, ApplicationUser appUser);

        public Task ChangeName(int id, AddTrainingProgramInputModel model);

        public Task DeleteProgramByIdAsync(int id);
    }
}