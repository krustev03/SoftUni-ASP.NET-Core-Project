namespace FitnessHub.Services.Data.Tests
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.Equipments;
    using FitnessHub.Web.ViewModels.Orders;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    public class OrderServiceTests : BaseServiceTest
    {
        // async Task AddOrderAsync(OrderInputModel orderInputModel, string userId)
        [Fact] // 1. async Task AddOrderAsync(OrderInputModel orderInputModel, string userId)
        public async void AddOrderAsync_ShouldAddOrderInDatabase()
        {
            // Arrange
            var imageSuplement1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "testSuplement1.jpg");

            var modelSuplement1 = new CreateSuplementInputModel()
            {
                Name = "Protein",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = imageSuplement1,
            };

            var imageSuplement2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "testSuplement2.jpg");

            var modelSuplement2 = new CreateSuplementInputModel()
            {
                Name = "Creatin",
                Weight = "300",
                Price = "10.00",
                Description = "The best suplement in the universe.",
                Image = imageSuplement2,
            };

            var imageEquipment1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "testEquipment1.jpg");

            var modelEquipment1 = new CreateEquipmentInputModel()
            {
                Name = "Straight bar",
                Price = "30.00",
                Description = "The best equipment in the universe.",
                Image = imageEquipment1,
            };

            var imageEquipment2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "testEquipment2.jpg");

            var modelEquipment2 = new CreateEquipmentInputModel()
            {
                Name = "Bench press",
                Price = "40.00",
                Description = "The best equipment in the universe.",
                Image = imageEquipment2,
            };
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.Context);
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var userSuplementsRepository = new EfDeletableEntityRepository<UserSuplement>(this.Context);
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.Context);
            var userEquipmentsRepository = new EfDeletableEntityRepository<UserEquipment>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, usersRepository, userSuplementsRepository, imagesRepository);
            var equipmentService = new EquipmentService(equipmentsRepository, usersRepository, userEquipmentsRepository, imagesRepository);
            var ordersRepository = new EfDeletableEntityRepository<Order>(this.Context);
            var orderEquipmentsRepository = new EfDeletableEntityRepository<OrderEquipment>(this.Context);
            var orderSuplementsRepository = new EfDeletableEntityRepository<OrderSuplement>(this.Context);
            var orderService = new OrderService(
                ordersRepository,
                userEquipmentsRepository,
                userSuplementsRepository,
                orderEquipmentsRepository,
                orderSuplementsRepository,
                equipmentsRepository,
                suplementsRepository,
                usersRepository);

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await suplementService.AddSuplementAsync(modelSuplement1, user.Id, "~/images");
            await suplementService.AddSuplementAsync(modelSuplement2, user.Id, "~/images");
            await equipmentService.AddEquipmentAsync(modelEquipment1, user.Id, "~/images");
            await equipmentService.AddEquipmentAsync(modelEquipment2, user.Id, "~/images");

            await suplementService.AddSuplementToCart(1, user.Id);
            await suplementService.AddSuplementToCart(2, user.Id);
            await equipmentService.AddEquipmentToCart(1, user.Id);
            await equipmentService.AddEquipmentToCart(2, user.Id);

            var modelOrder = new OrderInputModel()
            {
                FirstName = "Petyr",
                LastName = "Krystev",
                BirthDay = "14",
                BirthMonth = "01",
                BirthYear = "1950",
                City = "Montana",
                CityCode = "3400",
                Adress = "bul. Treti Mart 63",
            };

            // Act
            await orderService.AddOrderAsync(modelOrder, user.Id);
            var resultCount = ordersRepository.All().ToList().Count();
            var order = await ordersRepository.GetByIdWithDeletedAsync(1);
            var expectedCount = 1;
            var expectedFirstName = "Petyr";
            var expectedLastName = "Krystev";
            var expectedBirthDate = "14.1.1950";
            var expectedCity = "Montana";
            var expectedCityCode = "3400";
            var expectedAdress = "bul. Treti Mart 63";

            // Assert
            Assert.Equal(expectedCount, resultCount);
            Assert.Equal(expectedFirstName, order.FirstName);
            Assert.Equal(expectedLastName, order.LastName);
            Assert.Equal(expectedBirthDate, order.BirthDate.ToString("d/M/yyyy"));
            Assert.Equal(expectedCity, order.City);
            Assert.Equal(expectedCityCode, order.CityCode);
            Assert.Equal(expectedAdress, order.Adress);
        }
    }
}
