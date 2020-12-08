namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.News;

    public interface INewsService
    {
        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3);

        IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3);

        T GetNewsById<T>(int newsId);

        int GetCount();

        public int GetFilteredCount(string searchString);

        Task AddNewsAsync(NewsInputModel serviceInputModel);

        Task EditNews(int newsId, NewsInputModel model);

        Task DeleteNewsByIdAsync(int newsId);
    }
}
