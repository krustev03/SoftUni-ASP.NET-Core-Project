namespace FitnessHub.Web.ViewModels.Suplements
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class SuplementDetailsViewModel : IMapFrom<Suplement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
