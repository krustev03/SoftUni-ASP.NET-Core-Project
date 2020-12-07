namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Suplements;

    public interface ISuplementService
    {
        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3);

        T GetSuplementById<T>(int suplementId);

        int GetCount();

        public int GetFilteredCount(string searchString);

        Task AddSuplementAsync(CreateSuplementInputModel model, string userId, string imagePath);

        Task EditSuplement(EditSuplementInputModel model, int suplementId, string userId);

        Task DeleteSuplementByIdAsync(int suplementId);

        Task AddSuplementToCart(int suplementId, string userId);
    }
}
