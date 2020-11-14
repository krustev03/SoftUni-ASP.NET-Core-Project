namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.MyCart;

    public interface IMyCartService
    {
        public IEnumerable<EquipmentCartViewModel> GetUserEquipments(ApplicationUser appUser);

        public IEnumerable<SuplementCartViewModel> GetUserSuplements(ApplicationUser appUser);

        public decimal GetTotalPrice(ApplicationUser appUser);

        public Task RemoveEquipmentFromCartAsync(int id, string userId);

        public Task RemoveSuplementFromCartAsync(int id, string userId);
    }
}
