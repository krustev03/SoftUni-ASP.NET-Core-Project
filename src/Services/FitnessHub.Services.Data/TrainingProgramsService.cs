namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public class TrainingProgramsService : ITrainingProgramsService
    {
        private readonly IDeletableEntityRepository<TrainingProgram> trainingProgramsRepository;

        public TrainingProgramsService(IDeletableEntityRepository<TrainingProgram> trainingProgramsRepository)
        {
            this.trainingProgramsRepository = trainingProgramsRepository;
        }

        public async Task AddTrainingProgramAsync(AddTrainingProgramInputModel programModel, ApplicationUser appUser)
        {
            var trainingProgram = new TrainingProgram()
            {
                Name = programModel.Name,
                Trainings = new List<Training>(),
                CreatorId = appUser.Id,
            };

            await this.trainingProgramsRepository.AddAsync(trainingProgram);
            await this.trainingProgramsRepository.SaveChangesAsync();
        }
    }
}
