namespace FitnessHub.Web.ViewModels.Equipments
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class EquipmentDetailsViewModel : IMapFrom<Equipment>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
