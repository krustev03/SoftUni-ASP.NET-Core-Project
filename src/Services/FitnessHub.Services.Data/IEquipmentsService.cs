namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Equipments;

    public interface IEquipmentsService
    {
        public IEnumerable<T> GetAllEquipments<T>();

        public Task AddEquipmentAsync(EquipmentInputModel equipmentInputModel);

        public Task EditEquipment(int id, EquipmentInputModel equipmentInputModel);

        public T GetEquipmentDetails<T>(int id);

        public Task DeleteEquipmentByIdAsync(int id);

        public Task AddEquipmentToCart(int id, string userId);
    }
}
