namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class Equipment : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
