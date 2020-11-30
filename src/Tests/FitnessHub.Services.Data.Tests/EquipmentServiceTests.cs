namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using Xunit;

  // async Task<IEnumerable<CityListServiceModel>> AllCitiesAsync()

  // async Task<IEnumerable<CityListServiceModel>> AllCitiesByCountryAsync(int id)
    public class EquipmentServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task<IEnumerable<CityListServiceModel>> AllCitiesAsync()
        public async void AddEquipmentAsync_ShouldAddEquipmentInDatabase()
        {
            // Arrange
            var equipment = new Equipment()
            {
                Id = 23,
                Name = "Dumbel",
                Price = 19.99m,
                Description = "The best equipment in the world.",
            };

            // Act
            await this.Context.Equipments.AddAsync(equipment);
            await this.Context.SaveChangesAsync();
            var expected = this.Context.Equipments.Count();

            // Assert
            Assert.Equal(1, expected);
        }
    }
}
