namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.TrainerPosts;
    using Xunit;

    // async Task AddPostAsync(TrainerPostInputModel model, string userId)

    // async Task EditPost(int trainerPostId, TrainerPostInputModel model)

    // IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)

    // int GetCount()

    // async Task DeletePostByIdAsync(int id)
    public class TrainerPostServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task AddPostAsync(TrainerPostInputModel model, string userId)
        public async void AddPostAsync_ShouldAddTrainerPostInDatabase()
        {
            // Arrange
            var trainerPostsRepository = new EfDeletableEntityRepository<TrainerPost>(this.Context);
            var trainerPostService = new TrainerPostService(trainerPostsRepository);

            var model1 = new TrainerPostInputModel()
            {
                FirstName = "Sasho",
                LastName = "Mitov",
                Description = "The best trainer in the universe.",
            };

            // Act
            await trainerPostService.AddPostAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            var trainerPost = await trainerPostsRepository.GetByIdWithDeletedAsync(1);
            var expectedFirstName = "Sasho";
            var expectedLastName = "Mitov";
            var expectedDescription = "The best trainer in the universe.";

            // Assert
            Assert.Equal(expectedFirstName, trainerPost.FirstName);
            Assert.Equal(expectedLastName, trainerPost.LastName);
            Assert.Equal(expectedDescription, trainerPost.Description);
        }

        [Fact] // 2. async Task EditPost(int trainerPostId, TrainerPostInputModel model)
        public async void EditPost_ShouldEditTrainerPostInDatabase()
        {
            // Arrange
            var trainerPostsRepository = new EfDeletableEntityRepository<TrainerPost>(this.Context);
            var trainerPostService = new TrainerPostService(trainerPostsRepository);

            var model1 = new TrainerPostInputModel()
            {
                FirstName = "Sasho",
                LastName = "Mitov",
                Description = "The best trainer in the universe.",
            };

            var model2 = new TrainerPostInputModel()
            {
                FirstName = "Pesho",
                LastName = "Kirov",
                Description = "The best trainer buddy in the universe.",
            };

            await trainerPostService.AddPostAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            await trainerPostService.EditPost(1, model2);
            var trainerPost = await trainerPostsRepository.GetByIdWithDeletedAsync(1);

            var expectedFirstName = "Pesho";
            var expectedLastName = "Kirov";
            var expectedDescription = "The best trainer buddy in the universe.";

            // Assert
            Assert.Equal(expectedFirstName, trainerPost.FirstName);
            Assert.Equal(expectedLastName, trainerPost.LastName);
            Assert.Equal(expectedDescription, trainerPost.Description);
        }

        [Fact] // 3. IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        public async void GetAllForPaging_ShouldReturnAllTrainerPostsInOnePage()
        {
            // Arrange
            var trainerPostsRepository = new EfDeletableEntityRepository<TrainerPost>(this.Context);
            var trainerPostService = new TrainerPostService(trainerPostsRepository);

            var model1 = new TrainerPostInputModel()
            {
                FirstName = "Sasho",
                LastName = "Mitov",
                Description = "The best trainer in the universe.",
            };

            var model2 = new TrainerPostInputModel()
            {
                FirstName = "Pesho",
                LastName = "Kirov",
                Description = "The best trainer in the universe.",
            };

            var model3 = new TrainerPostInputModel()
            {
                FirstName = "Ivcho",
                LastName = "Spasov",
                Description = "The best trainer in the universe.",
            };

            var model4 = new TrainerPostInputModel()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Description = "The best trainer in the universe.",
            };

            await trainerPostService.AddPostAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainerPostService.AddPostAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainerPostService.AddPostAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainerPostService.AddPostAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            var resultCount = trainerPostService.GetAllForPaging<TrainerPostViewModel>(1, 3).Count();
            var expectedCount = 3;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 4. int GetCount()
        public async void GetCount_ShouldReturnTrainerPostsCount()
        {
            // Arrange
            var trainerPostsRepository = new EfDeletableEntityRepository<TrainerPost>(this.Context);
            var trainerPostService = new TrainerPostService(trainerPostsRepository);

            var model1 = new TrainerPostInputModel()
            {
                FirstName = "Sasho",
                LastName = "Mitov",
                Description = "The best trainer in the universe.",
            };

            var model2 = new TrainerPostInputModel()
            {
                FirstName = "Pesho",
                LastName = "Kirov",
                Description = "The best trainer in the universe.",
            };

            var model3 = new TrainerPostInputModel()
            {
                FirstName = "Ivcho",
                LastName = "Spasov",
                Description = "The best trainer in the universe.",
            };

            var model4 = new TrainerPostInputModel()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Description = "The best trainer in the universe.",
            };

            await trainerPostService.AddPostAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainerPostService.AddPostAsync(model2, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainerPostService.AddPostAsync(model3, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainerPostService.AddPostAsync(model4, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            var resultCount = trainerPostService.GetCount();
            var expectedCount = 4;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 5. async Task DeletePostByIdAsync(int id)
        public async void DeletePostByIdAsync_ShouldDeleteTrainerPostInDatabase()
        {
            // Arrange
            var model1 = new TrainerPostInputModel()
            {
                FirstName = "Sasho",
                LastName = "Mitov",
                Description = "The best trainer in the universe.",
            };

            var trainerPostsRepository = new EfDeletableEntityRepository<TrainerPost>(this.Context);
            var trainerPostService = new TrainerPostService(trainerPostsRepository);

            await trainerPostService.AddPostAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            await trainerPostService.DeletePostByIdAsync(1);
            var resultCount = trainerPostsRepository.All().ToList().Count();
            var expectedCount = 0;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
