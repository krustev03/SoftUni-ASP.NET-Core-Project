namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Suplements;

    public interface ISuplementService
    {
        IEnumerable<T> GetAllSuplements<T>();

        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        T GetSuplementById<T>(int suplementId);

        int GetCount();

        Task AddSuplementAsync(CreateSuplementInputModel model, string userId, string imagePath);

        Task EditSuplement(EditSuplementInputModel model, int suplementId, string userId);

        Task DeleteSuplementByIdAsync(int suplementId);

        Task AddSuplementToCart(int suplementId, string userId);
    }
}
