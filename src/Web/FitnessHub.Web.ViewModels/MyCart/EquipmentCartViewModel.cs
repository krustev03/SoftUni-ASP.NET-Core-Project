namespace FitnessHub.Web.ViewModels.MyCart
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class EquipmentCartViewModel : IMapFrom<Equipment>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}
