namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Suplements;

    public interface ISuplementService
    {
        public IEnumerable<T> GetAllSuplements<T>();

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        public int GetCount();

        public Task AddSuplementAsync(SuplementInputModel model, string userId, string imagePath);

        public Task EditSuplement(SuplementInputModel model, int suplementId, string userId, string imagePath);

        public T GetSuplementDetails<T>(int id);

        public Task DeleteSuplementByIdAsync(int id);

        public Task AddSuplementToCart(int id, string userId);
    }
}
