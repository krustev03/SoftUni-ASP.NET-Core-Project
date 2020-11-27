namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class OrderEquipment : BaseDeletableModel<int>
    {
        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        public int EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
