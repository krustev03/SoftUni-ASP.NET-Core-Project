namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class Suplement : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
