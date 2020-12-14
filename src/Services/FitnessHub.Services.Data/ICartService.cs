namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Cart;

    public interface ICartService
    {
        IEnumerable<EquipmentCartViewModel> GetUserEquipments(string userId);

        IEnumerable<SuplementCartViewModel> GetUserSuplements(string userId);

        decimal GetTotalPrice(string userId);

        Task RemoveEquipmentFromCartAsync(int equipmentId, string userId);

        Task RemoveSuplementFromCartAsync(int suplementId, string userId);
    }
}
