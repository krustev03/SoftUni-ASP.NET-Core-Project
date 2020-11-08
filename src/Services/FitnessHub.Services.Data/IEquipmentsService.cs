namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Equipments;

    public interface IEquipmentsService
    {
        public IEnumerable<T> GetAllEquipments<T>();

        public Task AddEquipmentAsync(AddEquipmentInputModel equipmentInputModel, ApplicationUser appUser);

        public T GetEquipmentDetails<T>(int id);

        public Task DeleteEquipmentByIdAsync(int id);
    }
}
