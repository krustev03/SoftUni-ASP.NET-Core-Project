namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class Service : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string SellerId { get; set; }

        public virtual ApplicationUser Seller { get; set; }
    }
}
