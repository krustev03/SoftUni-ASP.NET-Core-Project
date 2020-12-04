namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Equipments;

    public interface IEquipmentService
    {
        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        T GetEquipmentById<T>(int equipmentId);

        int GetCount();

        Task AddEquipmentAsync(EquipmentInputModel model, string userId, string imagePath);

        Task EditEquipment(EquipmentInputModel model, int equipmentId, string userId, string imagePath);

        Task DeleteEquipmentByIdAsync(int equipmentId);

        Task AddEquipmentToCart(int equipmentId, string userId);
    }
}
