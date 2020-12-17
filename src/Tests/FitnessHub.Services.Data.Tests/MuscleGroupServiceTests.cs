namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using Xunit;

    // IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
    public class MuscleGroupServiceTests : BaseServiceTest
    {
        [Fact] // 1. IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        public async void GetAllAsKeyValuePairs_ShouldReturnMuscleGroupsAsKeyValuePairs()
        {
            // Arrange
            var muscleGrousRepository = new EfRepository<MuscleGroup>(this.context);
            var muscleGroupService = new MuscleGroupService(muscleGrousRepository);
            await muscleGrousRepository.AddAsync(new MuscleGroup { Name = "Chest" });
            await muscleGrousRepository.AddAsync(new MuscleGroup { Name = "Back" });
            await muscleGrousRepository.AddAsync(new MuscleGroup { Name = "Legs" });
            await muscleGrousRepository.AddAsync(new MuscleGroup { Name = "Biceps" });
            await muscleGrousRepository.AddAsync(new MuscleGroup { Name = "Triceps" });
            await muscleGrousRepository.AddAsync(new MuscleGroup { Name = "Shoulders" });
            await muscleGrousRepository.SaveChangesAsync();

            // Act
            var result = muscleGroupService.GetAllAsKeyValuePairs().ToList();
            var resultCount = result.Count();
            var expectedCount = 6;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
