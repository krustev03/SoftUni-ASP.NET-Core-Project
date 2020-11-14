namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class Order : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AdditionalComment { get; set; }

        public string Adress { get; set; }

        public decimal Price { get; set; }
    }
}
