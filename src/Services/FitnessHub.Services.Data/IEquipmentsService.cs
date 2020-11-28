namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Equipments;

    public interface IEquipmentsService
    {
        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        public IEnumerable<T> GetAllEquipments<T>();

        public int GetCount();

        public Task AddEquipmentAsync(EquipmentInputModel model, string userId, string imagePath);

        public Task EditEquipment(EquipmentInputModel model, int equipmentId, string userId, string imagePath);

        public T GetEquipmentDetails<T>(int id);

        public Task DeleteEquipmentByIdAsync(int id);

        public Task AddEquipmentToCart(int id, string userId);
    }
}
