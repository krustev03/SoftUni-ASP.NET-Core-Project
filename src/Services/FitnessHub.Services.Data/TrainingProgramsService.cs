namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
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

        public IEnumerable<T> GetAllForPaging<T>(int page, string userId, int itemsPerPage = 3)
        {
            var trainingPrograms = this.trainingProgramsRepository.AllAsNoTracking()
                .Where(x => x.CreatorId == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return trainingPrograms;
        }

        public int GetCount()
        {
            return this.trainingProgramsRepository.All().Count();
        }

        public async Task DeleteProgramByIdAsync(int programId)
        {
            var program = this.trainingProgramsRepository.All().Where(x => x.Id == programId).FirstOrDefault();
            this.trainingProgramsRepository.Delete(program);
            await this.trainingProgramsRepository.SaveChangesAsync();
        }
    }
}
