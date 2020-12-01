namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.MyCart;

    public interface IMyCartService
    {
        public IEnumerable<EquipmentCartViewModel> GetUserEquipments(string userId);

        public IEnumerable<SuplementCartViewModel> GetUserSuplements(string userId);

        public decimal GetTotalPrice(string userId);

        public Task RemoveEquipmentFromCartAsync(int id, string userId);

        public Task RemoveSuplementFromCartAsync(int id, string userId);
    }
}
