namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.News;
    using Xunit;

    // async Task AddNewsAsync(NewsInputModel serviceInputModel)

    // async Task EditNews(int newsId, NewsInputModel model)

    // IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)

    // T GetNewsById<T>(int newsId)

    // int GetCount()

    // async Task DeleteNewsByIdAsync(int newsId)
    public class NewsServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task AddNewsAsync(NewsInputModel serviceInputModel)
        public async void AddNewsAsync_ShouldAddNewsInDatabase()
        {
            // Arrange
            var model = new NewsInputModel()
            {
                Title = "Novina",
                Content = "The best news in the universe now.",
            };
            var newsRepository = new EfDeletableEntityRepository<News>(this.Context);
            var newsService = new NewsService(newsRepository);

            // Act
            await newsService.AddNewsAsync(model);
            var news = await newsRepository.GetByIdWithDeletedAsync(1);
            var expectedTitle = "Novina";
            var expectedContent = "The best news in the universe now.";

            // Assert
            Assert.Equal(expectedTitle, news.Title);
            Assert.Equal(expectedContent, news.Content);
        }

        [Fact] // 2. async Task EditNews(int newsId, NewsInputModel model)
        public async void EditNews_ShouldEditGivenNewsInDatabase()
        {
            // Arrange
            var model1 = new NewsInputModel()
            {
                Title = "Novina",
                Content = "The best news in the universe now.",
            };

            var model2 = new NewsInputModel()
            {
                Title = "Nova Novina",
                Content = "The best news in the universe now and ever.",
            };
            var newsRepository = new EfDeletableEntityRepository<News>(this.Context);
            var newsService = new NewsService(newsRepository);
            await newsService.AddNewsAsync(model1);

            // Act
            await newsService.EditNews(1, model2);
            var news = await newsRepository.GetByIdWithDeletedAsync(1);
            var expectedTitle = "Nova Novina";
            var expectedContent = "The best news in the universe now and ever.";

            // Assert
            Assert.Equal(expectedTitle, news.Title);
            Assert.Equal(expectedContent, news.Content);
        }

        [Fact] // 3. IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        public async void GetAllForPaging_ShouldReturnAllNewsInOnePage()
        {
            // Arrange
            var model1 = new NewsInputModel()
            {
                Title = "Novina",
                Content = "The best news in the universe now.",
            };

            var model2 = new NewsInputModel()
            {
                Title = "Nova Novina",
                Content = "The best news in the universe now and ever.",
            };
            var newsRepository = new EfDeletableEntityRepository<News>(this.Context);
            var newsService = new NewsService(newsRepository);
            await newsService.AddNewsAsync(model1);
            await newsService.AddNewsAsync(model2);

            // Act
            var resultCount = newsService.GetAllForPaging<NewsViewModel>(1, 3).Count();
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 4. T GetNewsById<T>(int newsId)
        public async void GetNewsById_ShouldGetNewsByIdInDatabase()
        {
            // Arrange
            var model = new NewsInputModel()
            {
                Title = "Novina",
                Content = "The best news in the universe now.",
            };
            var newsRepository = new EfDeletableEntityRepository<News>(this.Context);
            var newsService = new NewsService(newsRepository);

            await newsService.AddNewsAsync(model);

            // Act
            var news = newsService.GetNewsById<NewsViewModel>(1);
            var expectedTitle = "Novina";
            var expectedContent = "The best news in the universe now.";

            // Assert
            Assert.Equal(expectedTitle, news.Title);
            Assert.Equal(expectedContent, news.Content);
        }

        [Fact] // 5. int GetCount()
        public async void GetCount_ShouldReturnNewsCount()
        {
            // Arrange
            var model1 = new NewsInputModel()
            {
                Title = "Novina",
                Content = "The best news in the universe now.",
            };

            var model2 = new NewsInputModel()
            {
                Title = "Nova Novina",
                Content = "The best news in the universe now and ever.",
            };
            var newsRepository = new EfDeletableEntityRepository<News>(this.Context);
            var newsService = new NewsService(newsRepository);
            await newsService.AddNewsAsync(model1);
            await newsService.AddNewsAsync(model2);

            // Act
            var resultCount = newsService.GetCount();
            var expectedCount = 2;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact] // 6. async Task DeleteNewsByIdAsync(int id)
        public async void DeleteNewsByIdAsync_ShouldDeleteGivenNewsInDatabase()
        {
            // Arrange
            var model1 = new NewsInputModel()
            {
                Title = "Novina",
                Content = "The best news in the universe now.",
            };

            var model2 = new NewsInputModel()
            {
                Title = "Nova Novina",
                Content = "The best news in the universe now and ever.",
            };
            var newsRepository = new EfDeletableEntityRepository<News>(this.Context);
            var newsService = new NewsService(newsRepository);
            await newsService.AddNewsAsync(model1);
            await newsService.AddNewsAsync(model2);

            // Act
            await newsService.DeleteNewsByIdAsync(1);
            var resultCount = newsRepository.All().ToList().Count();
            var expectedCount = 1;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
