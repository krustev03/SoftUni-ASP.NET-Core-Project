﻿namespace FitnessHub.Services.Data.Tests
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.Equipments;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    public class MyCartServiceTests : BaseServiceTest
    {
        // IEnumerable<EquipmentCartViewModel> GetUserEquipments(string userId)

        // IEnumerable<SuplementCartViewModel> GetUserSuplements(string userId)

        // public decimal GetTotalPrice(string userId)

        // public async Task RemoveEquipmentFromCartAsync(int id, string userId)

        // public async Task RemoveSuplementFromCartAsync(int id, string userId)
        [Fact] // 1. IEnumerable<EquipmentCartViewModel> GetUserEquipments(string userId)
        public async void GetUserEquipments_ShouldGetAllTheEquipmentsInTheCartOfAUser()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userEquipmentsRepository = new EfDeletableEntityRepository<UserEquipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, usersRepository, userEquipmentsRepository, imagesRepository, null);
            var cartService = new CartService(equipmentsRepository, null, userEquipmentsRepository, null);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image1,
            };

            var image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test2.png");

            var model2 = new CreateEquipmentInputModel()
            {
                Name = "Lost",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image2,
            };

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await equipmentService.AddEquipmentAsync(model1, user.Id, "~/images");
            await equipmentService.AddEquipmentAsync(model2, user.Id, "~/images");

            await equipmentService.AddEquipmentToCart(1, user.Id);
            await equipmentService.AddEquipmentToCart(2, user.Id);

            // Act
            var result = cartService.GetUserEquipments(user.Id);
            var resultCount = result.Count();
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 2. IEnumerable<SuplementCartViewModel> GetUserSuplements(string userId)
        public async void GetUserSuplements_ShouldGetAllTheSuplementsInTheCartOfAUser()
        {
            // Arrange
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userSuplementsRepository = new EfDeletableEntityRepository<UserSuplement>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var suplementService = new SuplementService(suplementsRepository, usersRepository, userSuplementsRepository, imagesRepository, null);
            var cartService = new CartService(null, suplementsRepository, null, userSuplementsRepository);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image1,
            };

            var image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test2.png");

            var model2 = new CreateSuplementInputModel()
            {
                Name = "Lost",
                Weight = "200",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image2,
            };

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await suplementService.AddSuplementAsync(model1, user.Id, "~/images");
            await suplementService.AddSuplementAsync(model2, user.Id, "~/images");

            await suplementService.AddSuplementToCart(1, user.Id);
            await suplementService.AddSuplementToCart(2, user.Id);

            // Act
            var result = cartService.GetUserSuplements(user.Id);
            var resultCount = result.Count();
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 3. public decimal GetTotalPrice(string userId)
        public async void GetTotalPrice_ShouldGetTheTotalPriceOfTheItemsInTheCart()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userEquipmentsRepository = new EfDeletableEntityRepository<UserEquipment>(this.context);
            var userSuplementsRepository = new EfDeletableEntityRepository<UserSuplement>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, usersRepository, userEquipmentsRepository, imagesRepository, null);
            var suplementService = new SuplementService(suplementsRepository, usersRepository, userSuplementsRepository, imagesRepository, null);
            var cartService = new CartService(equipmentsRepository, suplementsRepository, userEquipmentsRepository, userSuplementsRepository);

            var imageEquipment = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var modelEquipment = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "10.00",
                Description = "The best equipment in the universe.",
                Image = imageEquipment,
            };

            var imageSuplement = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var modelSuplement = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "300",
                Price = "30.00",
                Description = "The best suplement in the universe.",
                Image = imageSuplement,
            };

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await equipmentService.AddEquipmentAsync(modelEquipment, user.Id, "~/images");
            await suplementService.AddSuplementAsync(modelSuplement, user.Id, "~/images");

            await equipmentService.AddEquipmentToCart(1, user.Id);
            await equipmentService.AddEquipmentToCart(1, user.Id);

            await suplementService.AddSuplementToCart(1, user.Id);
            await suplementService.AddSuplementToCart(1, user.Id);

            // Act
            var result = cartService.GetTotalPrice(user.Id);
            var expected = 80.00m;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact] // 4. public async Task RemoveEquipmentFromCartAsync(int id, string userId)
        public async void RemoveEquipmentFromCartAsync_ShouldRemoveTheEquipmentFromTheCar()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userEquipmentsRepository = new EfDeletableEntityRepository<UserEquipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, usersRepository, userEquipmentsRepository, imagesRepository, null);
            var cartService = new CartService(equipmentsRepository, null, userEquipmentsRepository, null);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image1,
            };

            var image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test2.png");

            var model2 = new CreateEquipmentInputModel()
            {
                Name = "Lost",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image2,
            };

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await equipmentService.AddEquipmentAsync(model1, user.Id, "~/images");
            await equipmentService.AddEquipmentAsync(model2, user.Id, "~/images");

            await equipmentService.AddEquipmentToCart(1, user.Id);
            await equipmentService.AddEquipmentToCart(2, user.Id);

            // Act
            await cartService.RemoveEquipmentFromCartAsync(1, user.Id);
            var resultCount = cartService.GetUserEquipments(user.Id).Count();
            var expectedCount = 1;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 5. public async Task RemoveSuplementFromCartAsync(int id, string userId)
        public async void RemoveSuplementFromCartAsync_ShouldRemoveTheSuplementFromTheCar()
        {
            // Arrange
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userSuplementsRepository = new EfDeletableEntityRepository<UserSuplement>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var suplementService = new SuplementService(suplementsRepository, usersRepository, userSuplementsRepository, imagesRepository, null);
            var cartService = new CartService(null, suplementsRepository, null, userSuplementsRepository);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image1,
            };

            var image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test2.png");

            var model2 = new CreateSuplementInputModel()
            {
                Name = "Lost",
                Weight = "200",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image2,
            };

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await suplementService.AddSuplementAsync(model1, user.Id, "~/images");
            await suplementService.AddSuplementAsync(model2, user.Id, "~/images");

            await suplementService.AddSuplementToCart(1, user.Id);
            await suplementService.AddSuplementToCart(2, user.Id);

            // Act
            await cartService.RemoveSuplementFromCartAsync(1, user.Id);
            var resultCount = cartService.GetUserSuplements(user.Id).Count();
            var expectedCount = 1;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
