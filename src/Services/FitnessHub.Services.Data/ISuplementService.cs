namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Suplements;

    public interface ISuplementService
    {
        IEnumerable<T> GetAllSuplements<T>();

        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        int GetCount();

        Task AddSuplementAsync(SuplementInputModel model, string userId, string imagePath);

        Task EditSuplement(SuplementInputModel model, int suplementId, string userId, string imagePath);

        Task DeleteSuplementByIdAsync(int suplementId);

        Task AddSuplementToCart(int suplementId, string userId);
    }
}
