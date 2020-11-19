namespace FitnessHub.Data.Models
{
    public class OrderEquipment
    {
        public int Id { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        public int EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
