namespace FitnessHub.Web.ViewModels.Equipments
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class EditEquipmentInputModel : BaseEquipmentInputModel, IMapFrom<Equipment>
    {
        public int Id { get; set; }
    }
}
