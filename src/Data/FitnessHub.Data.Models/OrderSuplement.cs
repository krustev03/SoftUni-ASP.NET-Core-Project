namespace FitnessHub.Data.Models
{
    public class OrderSuplement
    {
        public int Id { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        public int SuplementId { get; set; }

        public virtual Suplement Suplement { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
