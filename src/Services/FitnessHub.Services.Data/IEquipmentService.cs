namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Equipments;

    public interface IEquipmentService
    {
        IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3);

        T GetEquipmentById<T>(int equipmentId);

        int GetCount();

        public int GetFilteredCount(string searchString);

        Task AddEquipmentAsync(CreateEquipmentInputModel model, string userId, string imagePath);

        Task EditEquipment(EditEquipmentInputModel model, int equipmentId, string userId);

        Task DeleteEquipmentByIdAsync(int equipmentId);

        Task AddEquipmentToCart(int equipmentId, string userId);
    }
}
