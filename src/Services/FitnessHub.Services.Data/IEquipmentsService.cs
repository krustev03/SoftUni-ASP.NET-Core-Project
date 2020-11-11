namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Equipments;

    public interface IEquipmentsService
    {
        public IEnumerable<T> GetAllEquipments<T>();

        public Task AddEquipmentAsync(AddEquipmentInputModel equipmentInputModel);

        public T GetEquipmentDetails<T>(int id);

        public Task DeleteEquipmentByIdAsync(int id);

        public Task AddEquipmentToCart(int id, string userId);
    }
}
