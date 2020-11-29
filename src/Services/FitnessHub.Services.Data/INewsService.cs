namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.News;

    public interface INewsService
    {
        public Task AddNewsAsync(NewsInputModel serviceInputModel);

        public Task EditNews(int newsId, NewsInputModel model);

        public IEnumerable<T> GetAllNews<T>();

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3);

        public int GetCount();

        public Task DeleteNewsByIdAsync(int newsId);
    }
}
