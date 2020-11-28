namespace FitnessHub.Data.Models
{
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    public class Suplement : BaseDeletableModel<int>
    {
        public Suplement()
        {
            this.Users = new HashSet<UserSuplement>();
            this.Orders = new HashSet<OrderSuplement>();
        }

        public string Name { get; set; }

        public int Weight { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<UserSuplement> Users { get; set; }

        public virtual ICollection<OrderSuplement> Orders { get; set; }
    }
}
