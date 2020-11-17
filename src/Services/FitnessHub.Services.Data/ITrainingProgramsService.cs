namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface ITrainingProgramsService
    {
        public Task AddTrainingProgramAsync(AddTrainingProgramInputModel programModel, ApplicationUser appUser);
    }
}