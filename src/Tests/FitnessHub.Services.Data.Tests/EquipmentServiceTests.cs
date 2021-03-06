﻿namespace FitnessHub.Services.Data.Tests
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.Equipments;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    // async Task AddEquipmentAsync(EquipmentInputModel model, string userId, string imagePath)

    // async Task EditEquipment(EquipmentInputModel model, int equipmentId, string userId, string imagePath)

    // IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)

    // IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)

    // T GetEquipmentById<T>(int equipmentId)

    // int GetCount()

    // int GetFilteredCount(string searchString)

    // async void AddEquipmentToCart()

    // async Task DeleteEquipmentByIdAsync(int id)
    public class EquipmentServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task AddEquipmentAsync(EquipmentInputModel model, string userId, string imagePath)
        public async void AddEquipmentAsync_ShouldAddEquipmentInDatabase()
        {
            // Arrange
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test.jpg");

            var model = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image,
            };
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

            // Act
            await equipmentService.AddEquipmentAsync(model, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            var equipment = await equipmentsRepository.GetByIdWithDeletedAsync(1);
            var expectedName = "Peika";
            var expectedPrice = 20.00m;
            var expectedDescription = "The best equipment in the universe.";
            var expectedImage = new Image()
            {
                AddedByUserId = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Extension = "jpg",
                EquipmentId = 1,
            };

            // Assert
            Assert.Equal(expectedName, equipment.Name);
            Assert.Equal(expectedPrice, equipment.Price);
            Assert.Equal(expectedDescription, equipment.Description);
            Assert.Equal(expectedImage.AddedByUserId, equipment.Image.AddedByUserId);
            Assert.Equal(expectedImage.Extension, equipment.Image.Extension);
            Assert.Equal(expectedImage.EquipmentId, equipment.Image.EquipmentId);
        }

        [Fact] // 2. async Task EditEquipment(EquipmentInputModel model, int equipmentId, string userId, string imagePath)
        public async void EditEquipment_ShouldEditEquipmentInDatabase()
        {
            // Arrange
            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image1,
            };
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

            await equipmentService.AddEquipmentAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            var image2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test2.png");

            var model2 = new EditEquipmentInputModel()
            {
                Name = "Lost",
                Price = "21.00",
                Description = "The best equipment in the world.",
            };

            // Act
            await equipmentService.EditEquipment(model2, 1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            var equipment = await equipmentsRepository.GetByIdWithDeletedAsync(1);

            var expectedName = "Lost";
            var expectedPrice = 21.00m;
            var expectedDescription = "The best equipment in the world.";

            // Assert
            Assert.Equal(expectedName, equipment.Name);
            Assert.Equal(expectedPrice, equipment.Price);
            Assert.Equal(expectedDescription, equipment.Description);
        }

        [Fact] // 3. IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        public async void GetAllForPaging_ShouldReturnAllEquipmentsInOnePage()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateEquipmentInputModel()
            {
                Name = "Makara",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateEquipmentInputModel()
            {
                Name = "Lejanka",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image4,
            };

            await equipmentService.AddEquipmentAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = equipmentService.GetAllForPaging<EquipmentViewModel>(1, 3).Count();
            var expectedCount = 3;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 4. IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)
        public async void GetAllWithFilterForPaging_ShouldReturnFilteredEquipmentsInOnePage()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateEquipmentInputModel()
            {
                Name = "Madara",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateEquipmentInputModel()
            {
                Name = "Lejanka",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image4,
            };

            await equipmentService.AddEquipmentAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = equipmentService.GetAllWithFilterForPaging<EquipmentViewModel>(1, "ka", 3).Count();
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 5. T GetEquipmentById<T>(int equipmentId)
        public async void GetEquipmentById_ShouldGetEquipmentByIdInDatabase()
        {
            // Arrange
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test.jpg");

            var model = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image,
            };
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

            await equipmentService.AddEquipmentAsync(model, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var equipment = equipmentService.GetEquipmentById<EquipmentViewModel>(1);
            var expectedName = "Peika";
            var expectedPrice = 20.00m;
            var expectedDescription = "The best equipment in the universe.";

            // Assert
            Assert.Equal(expectedName, equipment.Name);
            Assert.Equal(expectedPrice, equipment.Price);
            Assert.Equal(expectedDescription, equipment.Description);
        }

        [Fact] // 6. int GetCount()
        public async void GetCount_ShouldReturnAllEquipmentsCount()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateEquipmentInputModel()
            {
                Name = "Makara",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateEquipmentInputModel()
            {
                Name = "Lejanka",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image4,
            };

            await equipmentService.AddEquipmentAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = equipmentService.GetCount();
            var expectedCount = 4;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 7. int GetFilteredCount(string searchString)
        public async void GetFilteredCount_ShouldReturnFilteredEquipmentsCount()
        {
            // Arrange
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, null, null, imagesRepository, null);

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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateEquipmentInputModel()
            {
                Name = "Makara",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateEquipmentInputModel()
            {
                Name = "Lejanka",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image4,
            };

            await equipmentService.AddEquipmentAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await equipmentService.AddEquipmentAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = equipmentService.GetFilteredCount("ka");
            var expectedCount = 3;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 8. async Task AddEquipmentToCart(int id, string userId)
        public async void AddEquipmentToCart_ShouldAddEquipmentToCart()
        {
            // Arrange
            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image1,
            };
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userEquipmentsRepository = new EfDeletableEntityRepository<UserEquipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, usersRepository, userEquipmentsRepository, imagesRepository, null);

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await equipmentService.AddEquipmentAsync(model1, user.Id, "~/images");

            // Act
            await equipmentService.AddEquipmentToCart(1, user.Id);
            await equipmentService.AddEquipmentToCart(1, user.Id);
            var resultCount = userEquipmentsRepository.All().ToList().Count();
            var userEquipment = await userEquipmentsRepository.GetByIdWithDeletedAsync(1);
            var resultQuantity = userEquipment.Quantity;
            var expectedCount = 1;
            var expectedQuantity = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
            Assert.Equal(expectedQuantity, resultQuantity);
        }

        [Fact] // 9. async Task DeleteEquipmentByIdAsync(int id)
        public async void DeleteEquipmentByIdAsync_ShouldDeleteEquipmentInCartAndInDatabase()
        {
            // Arrange
            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateEquipmentInputModel()
            {
                Name = "Peika",
                Price = "20.00",
                Description = "The best equipment in the universe.",
                Image = image1,
            };
            var equipmentsRepository = new EfDeletableEntityRepository<Equipment>(this.context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.context);
            var userEquipmentsRepository = new EfDeletableEntityRepository<UserEquipment>(this.context);
            var imagesRepository = new EfRepository<Image>(this.context);
            var equipmentService = new EquipmentService(equipmentsRepository, usersRepository, userEquipmentsRepository, imagesRepository, null);

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await equipmentService.AddEquipmentAsync(model1, user.Id, "~/images");

            await equipmentService.AddEquipmentToCart(1, user.Id);
            await equipmentService.AddEquipmentToCart(1, user.Id);

            // Act
            await equipmentService.DeleteEquipmentByIdAsync(1);
            var resultEquipmentsCount = equipmentsRepository.All().ToList().Count();
            var resultUserEquipmentsCount = userEquipmentsRepository.All().ToList().Count();
            var expectedEquipmetsCount = 0;
            var expectedUserEquipmentsCount = 0;

            // Assert
            Assert.Equal(expectedEquipmetsCount, resultEquipmentsCount);
            Assert.Equal(expectedUserEquipmentsCount, resultUserEquipmentsCount);
        }
    }
}
