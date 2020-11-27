namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class UserEquipment : BaseDeletableModel<int>
    {
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public int EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
