namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class OrderSuplement : BaseDeletableModel<int>
    {
        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        public int SuplementId { get; set; }

        public virtual Suplement Suplement { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
