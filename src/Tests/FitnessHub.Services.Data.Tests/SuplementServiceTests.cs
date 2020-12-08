namespace FitnessHub.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Http.Internal;
    using Xunit;

    // async Task AddSuplementAsync(SuplementInputModel model, string userId, string imagePath)

    // async Task EditSuplement(SuplementInputModel model, int suplementId, string userId, string imagePath)

    // IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)

    // IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)

    // T GetSuplementById<T>(int suplementId)

    // int GetCount()

    // int GetFilteredCount(string searchString)

    // async void AddSuplementToCart()

    // async Task DeleteSuplementByIdAsync(int id)
    public class SuplementServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task AddSupplementAsync(SuplementInputModel model, string userId, string imagePath)
        public async void AddSuplementAsync_ShouldAddSuplementInDatabase()
        {
            // Arrange
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test.jpg");

            var model = new CreateSuplementInputModel()
            {
                Name = "Shake",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image,
            };
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            // Act
            await suplementService.AddSuplementAsync(model, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            var suplement = await suplementsRepository.GetByIdWithDeletedAsync(1);
            var expectedName = "Shake";
            var expectedWeight = 300;
            var expectedPrice = 20.00m;
            var expectedDescription = "The best suplement in the universe.";
            var expectedImage = new Image()
            {
                AddedByUserId = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Extension = "jpg",
                SuplementId = 1,
            };

            // Assert
            Assert.Equal(expectedName, suplement.Name);
            Assert.Equal(expectedWeight, suplement.Weight);
            Assert.Equal(expectedPrice, suplement.Price);
            Assert.Equal(expectedDescription, suplement.Description);
            Assert.Equal(expectedImage.AddedByUserId, suplement.Image.AddedByUserId);
            Assert.Equal(expectedImage.Extension, suplement.Image.Extension);
            Assert.Equal(expectedImage.EquipmentId, suplement.Image.EquipmentId);
        }

        [Fact] // 2. async Task EditSuplement(SuplementInputModel model, int suplementId, string userId, string imagePath)
        public async void EditSuplement_ShouldEditSuplementInDatabase()
        {
            // Arrange
            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Shake",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image1,
            };

            var model2 = new EditSuplementInputModel()
            {
                Name = "Creatin",
                Weight = "500",
                Price = "25.00",
                Description = "The best creatin in the universe.",
            };
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            await suplementService.AddSuplementAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            await suplementService.EditSuplement(model2, 1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            var suplement = await suplementsRepository.GetByIdWithDeletedAsync(1);

            var expectedName = "Creatin";
            var expectedPrice = 25.00m;
            var expectedWeight = 500;
            var expectedDescription = "The best creatin in the universe.";

            // Assert
            Assert.Equal(expectedName, suplement.Name);
            Assert.Equal(expectedWeight, suplement.Weight);
            Assert.Equal(expectedPrice, suplement.Price);
            Assert.Equal(expectedDescription, suplement.Description);
        }

        [Fact] // 3. IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        public async void GetAllForPaging_ShouldReturnAllSuplementsInOnePage()
        {
            // Arrange
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "100",
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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateSuplementInputModel()
            {
                Name = "Makara",
                Weight = "300",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateSuplementInputModel()
            {
                Name = "Lejanka",
                Weight = "400",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image4,
            };

            await suplementService.AddSuplementAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = suplementService.GetAllForPaging<SuplementViewModel>(1, 3).Count();
            var expectedCount = 3;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 4. IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)
        public async void GetAllWithFilterForPaging_ShouldReturnFilteredSuplementsInOnePage()
        {
            // Arrange
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "100",
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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateSuplementInputModel()
            {
                Name = "Madara",
                Weight = "300",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateSuplementInputModel()
            {
                Name = "Lejanka",
                Weight = "400",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image4,
            };

            await suplementService.AddSuplementAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = suplementService.GetAllWithFilterForPaging<SuplementViewModel>(1, "ka", 3).Count();
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 5. T GetSuplementById<T>(int suplementId)
        public async void GetSuplementById_ShouldGetSuplementByIdInDatabase()
        {
            // Arrange
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test.jpg");

            var model = new CreateSuplementInputModel()
            {
                Name = "Shake",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image,
            };
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            await suplementService.AddSuplementAsync(model, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var suplement = suplementService.GetSuplementById<SuplementViewModel>(1);
            var expectedName = "Shake";
            var expectedWeight = 300;
            var expectedPrice = 20.00m;
            var expectedDescription = "The best suplement in the universe.";

            // Assert
            Assert.Equal(expectedName, suplement.Name);
            Assert.Equal(expectedWeight, suplement.Weight);
            Assert.Equal(expectedPrice, suplement.Price);
            Assert.Equal(expectedDescription, suplement.Description);
        }

        [Fact] // 6. int GetCount()
        public async void GetCount_ShouldReturnAllSuplementsCount()
        {
            // Arrange
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "100",
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

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateSuplementInputModel()
            {
                Name = "Makara",
                Weight = "300",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateSuplementInputModel()
            {
                Name = "Lejanka",
                Weight = "400",
                Price = "21.00",
                Description = "The best suplement in the world.",
                Image = image4,
            };

            await suplementService.AddSuplementAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = suplementService.GetCount();
            var expectedCount = 4;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 7. int GetFilteredCount(string searchString)
        public async void GetFilteredCount_ShouldReturnFilteredSuplementsCount()
        {
            // Arrange
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementService = new SuplementService(suplementsRepository, null, null, imagesRepository);

            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Peika",
                Weight = "100",
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
                Description = "The best equipment in the world.",
                Image = image2,
            };

            var image3 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test3.png");

            var model3 = new CreateSuplementInputModel()
            {
                Name = "Makara",
                Weight = "300",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image3,
            };

            var image4 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test4.png");

            var model4 = new CreateSuplementInputModel()
            {
                Name = "Lejanka",
                Weight = "400",
                Price = "21.00",
                Description = "The best equipment in the world.",
                Image = image4,
            };

            await suplementService.AddSuplementAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");
            await suplementService.AddSuplementAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80", "~/images");

            // Act
            var resultCount = suplementService.GetFilteredCount("ka");
            var expectedCount = 3;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 8. async Task AddSuplementToCart(int id, string userId)
        public async void AddSuplementToCart_ShouldAddSuplementToCart()
        {
            // Arrange
            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Protein",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image1,
            };
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.Context);
            var userSuplementsRepository = new EfDeletableEntityRepository<UserSuplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementsService = new SuplementService(suplementsRepository, usersRepository, userSuplementsRepository, imagesRepository);

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await suplementsService.AddSuplementAsync(model1, user.Id, "~/images");

            // Act
            await suplementsService.AddSuplementToCart(1, user.Id);
            await suplementsService.AddSuplementToCart(1, user.Id);
            var resultCount = userSuplementsRepository.All().ToList().Count();
            var userSuplement = await userSuplementsRepository.GetByIdWithDeletedAsync(1);
            var resultQuantity = userSuplement.Quantity;
            var expectedCount = 1;
            var expectedQuantity = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
            Assert.Equal(expectedQuantity, resultQuantity);
        }

        [Fact] // 9. async Task DeleteSuplementByIdAsync(int id)
        public async void DeleteSuplementByIdAsync_ShouldDeleteSuplementInCartAndInDatabase()
        {
            // Arrange
            var image1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "test1.jpg");

            var model1 = new CreateSuplementInputModel()
            {
                Name = "Protein",
                Weight = "300",
                Price = "20.00",
                Description = "The best suplement in the universe.",
                Image = image1,
            };
            var suplementsRepository = new EfDeletableEntityRepository<Suplement>(this.Context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.Context);
            var userSuplementsRepository = new EfDeletableEntityRepository<UserSuplement>(this.Context);
            var imagesRepository = new EfRepository<Image>(this.Context);
            var suplementsService = new SuplementService(suplementsRepository, usersRepository, userSuplementsRepository, imagesRepository);

            var user = new ApplicationUser()
            {
                Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await usersRepository.AddAsync(user);

            await suplementsService.AddSuplementAsync(model1, user.Id, "~/images");

            await suplementsService.AddSuplementToCart(1, user.Id);
            await suplementsService.AddSuplementToCart(1, user.Id);

            // Act
            await suplementsService.DeleteSuplementByIdAsync(1);
            var resultSuplementsCount = suplementsRepository.All().ToList().Count();
            var resultUserSuplementsCount = userSuplementsRepository.All().ToList().Count();
            var expectedSuplementsCount = 0;
            var expectedUserSuplementsCount = 0;

            // Assert
            Assert.Equal(expectedSuplementsCount, resultSuplementsCount);
            Assert.Equal(expectedUserSuplementsCount, resultUserSuplementsCount);
        }
    }
}
