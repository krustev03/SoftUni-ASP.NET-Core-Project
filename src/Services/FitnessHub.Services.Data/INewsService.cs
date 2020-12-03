namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.News;

    public interface INewsService
    {
        Task AddNewsAsync(NewsInputModel serviceInputModel);

        Task EditNews(int newsId, NewsInputModel model);

        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3);

        int GetCount();

        Task DeleteNewsByIdAsync(int newsId);
    }
}
