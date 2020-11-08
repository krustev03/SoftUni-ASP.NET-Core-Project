namespace FitnessHub.Web.ViewModels.Services
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class ServiceDetailsViewModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
