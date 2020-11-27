namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class UserSuplement : BaseDeletableModel<int>
    {
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public int SuplementId { get; set; }

        public virtual Suplement Suplement { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
