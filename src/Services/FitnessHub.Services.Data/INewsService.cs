namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.News;

    public interface INewsService
    {
        public Task AddNewsAsync(AddNewsInputModel serviceInputModel);

        public IEnumerable<T> GetAllNews<T>();

        public Task DeleteNewsByIdAsync(int id);
    }
}
