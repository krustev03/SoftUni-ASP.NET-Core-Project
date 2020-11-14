namespace FitnessHub.Data.Models
{
    public class UserSuplement
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public int SuplementId { get; set; }

        public virtual Suplement Suplement { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
