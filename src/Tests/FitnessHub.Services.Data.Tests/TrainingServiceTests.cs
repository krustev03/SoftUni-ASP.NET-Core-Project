namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Xunit;

    // async Task AddTrainingAsync(int programId, string dayOfWeek)

    // T GetTrainingDetails<T>(int trainingId)

    // async Task DeleteTrainingByIdAsync(int trainingId)
    public class TrainingServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task AddTrainingAsync(int programId, string dayOfWeek)
        public async void AddTrainingAsync_ShouldAddTrainingToTrainingProgramInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            await trainingService.AddTrainingAsync(1, "Monday");
            var training = await trainingsRepository.GetByIdWithDeletedAsync(1);
            var expectedDayOfWeek = "Monday";
            var expectedProgramId = 1;

            // Assert
            Assert.Equal(expectedDayOfWeek, training.DayOfWeek);
            Assert.Equal(expectedProgramId, training.Id);
        }

        [Fact] // 2. T GetTrainingDetails<T>(int trainingId)
        public async void GetTraining_ShouldGetTheTrainingWithTheGivenId()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingService.AddTrainingAsync(1, "Monday");

            // Act
            var training = trainingService.GetTraining<TrainingDetailsViewModel>(1);
            var expectedDayOfWeek = "Monday";

            // Assert
            Assert.Equal(expectedDayOfWeek, training.DayOfWeek);
        }

        [Fact] // 3. async Task DeleteTrainingByIdAsync(int trainingId)
        public async void DeleteTrainingByIdAsync_ShouldDeleteTrainingInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingService.AddTrainingAsync(1, "Monday");

            // Act
            await trainingService.DeleteTrainingByIdAsync(1, 1);
            var trainingsCountInRepo = trainingsRepository.All().ToList().Count();
            var trainingProgramTrainingCount = trainingProgramsRepository.GetByIdWithDeletedAsync(1).Result.Trainings.Count;
            var expectedCount = 0;

            // Assert
            Assert.Equal(expectedCount, trainingsCountInRepo);
            Assert.Equal(expectedCount, trainingProgramTrainingCount);
        }
    }
}
