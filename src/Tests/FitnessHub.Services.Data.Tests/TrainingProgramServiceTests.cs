namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Xunit;

    public class TrainingProgramServiceTests : BaseServiceTest
    {
        // async Task AddTrainingProgramAsync(TrainingProgramInputModel programModel, string userId)

        // async Task ChangeName(int programId, TrainingProgramInputModel model)

        // IEnumerable<T> GetAllForPaging<T>(int page, string userId, int itemsPerPage = 3)

        // int GetCount()

        // async Task DeleteProgramByIdAsync(int programId)
        [Fact] // 1. async Task AddTrainingProgramAsync(TrainingProgramInputModel programModel, string userId)
        public async void AddTrainingProgramAsync_ShouldAddTrainingProgramInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            // Act
            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            var trainingProgram = await trainingProgramsRepository.GetByIdWithDeletedAsync(1);
            var expectedName = "Programa";

            // Assert
            Assert.Equal(expectedName, trainingProgram.Name);
        }

        [Fact] // 2. async Task ChangeName(int programId, TrainingProgramInputModel model)
        public async void ChangeName_ShouldChangeTrainingProgramName()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa1",
            };

            var model2 = new TrainingProgramInputModel()
            {
                Name = "Programa2",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            await trainingProgramService.ChangeName(1, model2);
            var trainingProgram = await trainingProgramsRepository.GetByIdWithDeletedAsync(1);

            var expectedName = "Programa2";

            // Assert
            Assert.Equal(expectedName, trainingProgram.Name);
        }

        [Fact] // 3. IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        public async void GetAllForPaging_ShouldReturnAllTrainingProgramsInOnePage()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa1",
            };

            var model2 = new TrainingProgramInputModel()
            {
                Name = "Programa2",
            };

            var model3 = new TrainingProgramInputModel()
            {
                Name = "Programa3",
            };

            var model4 = new TrainingProgramInputModel()
            {
                Name = "Programa4",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingProgramService.AddTrainingProgramAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingProgramService.AddTrainingProgramAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingProgramService.AddTrainingProgramAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            var resultCount = trainingProgramService.GetAllForPaging<TrainingProgramViewModel>(1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", 3).Count();
            var expectedCount = 3;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 4. int GetCount()
        public async void GetCount_ShouldReturnTrainingProgramsCount()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa1",
            };

            var model2 = new TrainingProgramInputModel()
            {
                Name = "Programa2",
            };

            var model3 = new TrainingProgramInputModel()
            {
                Name = "Programa3",
            };

            var model4 = new TrainingProgramInputModel()
            {
                Name = "Programa4",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingProgramService.AddTrainingProgramAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingProgramService.AddTrainingProgramAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingProgramService.AddTrainingProgramAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            var resultCount = trainingProgramService.GetCount();
            var expectedCount = 4;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 5. async Task DeleteProgramByIdAsync(int programId)
        public async void DeleteProgramByIdAsync_ShouldDeleteTrainingProgramInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            await trainingProgramService.DeleteProgramByIdAsync(1);
            var resultCount = trainingProgramsRepository.All().ToList().Count();
            var expectedCount = 0;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
