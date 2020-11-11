namespace FitnessHub.Data.Models
{
    public class UserEquipment
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public int EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
