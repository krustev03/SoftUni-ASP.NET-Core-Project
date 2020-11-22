namespace FitnessHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDeletableEntityRepository<Training> trainingsRepository;

        public TrainingsService(IDeletableEntityRepository<Training> trainingsRepository)
        {
            this.trainingsRepository = trainingsRepository;
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

        public T GetTrainingDetails<T>(int id)
        {
            var training = this.trainingsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return training;
        }
    }
}
