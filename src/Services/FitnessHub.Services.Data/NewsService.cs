namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.News;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task AddNewsAsync(AddNewsInputModel serviceInputModel)
        {
            var news = new News()
            {
                Title = serviceInputModel.Title,
                Content = serviceInputModel.Content,
            };

            await this.newsRepository.AddAsync(news);
            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllNews<T>()
        {
            return this.newsRepository.All().To<T>();
        }

        public async Task DeleteNewsByIdAsync(int id)
        {
            var news = this.newsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.newsRepository.Delete(news);
            await this.newsRepository.SaveChangesAsync();
        }
    }
}
