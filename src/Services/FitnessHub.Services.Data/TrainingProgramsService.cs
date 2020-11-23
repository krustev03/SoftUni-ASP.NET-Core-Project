namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task ChangeName(int id, AddTrainingProgramInputModel model)
        {
            var trainingProgram = this.trainingProgramsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            trainingProgram.Name = model.Name;

            this.trainingProgramsRepository.Update(trainingProgram);
            await this.trainingProgramsRepository.SaveChangesAsync();
        }

        public async Task DeleteProgramByIdAsync(int id)
        {
            var program = this.trainingProgramsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.trainingProgramsRepository.Delete(program);
            await this.trainingProgramsRepository.SaveChangesAsync();
        }
    }
}
