namespace FitnessHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class TrainingService : ITrainingService
    {
        private readonly IDeletableEntityRepository<Training> trainingsRepository;
        private readonly IDeletableEntityRepository<TrainingProgram> trainingProgramsRepository;

        public TrainingService(
            IDeletableEntityRepository<Training> trainingsRepository,
            IDeletableEntityRepository<TrainingProgram> trainingProgramsRepository)
        {
            this.trainingsRepository = trainingsRepository;
            this.trainingProgramsRepository = trainingProgramsRepository;
        }

        public async Task AddTrainingAsync(int programId, string dayOfWeek)
        {
            var training = new Training()
            {
                DayOfWeek = dayOfWeek,
                TrainingProgramId = programId,
            };

            await this.trainingsRepository.AddAsync(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public T GetTraining<T>(int trainingId)
        {
            var training = this.trainingsRepository.All().Where(x => x.Id == trainingId).To<T>().FirstOrDefault();

            return training;
        }

        public async Task DeleteTrainingByIdAsync(int trainingId, int programId)
        {
            var training = this.trainingsRepository.All().Where(x => x.Id == trainingId).FirstOrDefault();
            this.trainingsRepository.Delete(training);
            var program = await this.trainingProgramsRepository.GetByIdWithDeletedAsync(programId);
            program.Trainings.Remove(training);
            await this.trainingsRepository.SaveChangesAsync();
            await this.trainingProgramsRepository.SaveChangesAsync();
        }
    }
}
