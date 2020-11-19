namespace FitnessHub.Data.Models
{
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    public class Equipment : BaseDeletableModel<int>
    {
        public Equipment()
        {
            this.Users = new HashSet<UserEquipment>();
            this.Orders = new HashSet<OrderEquipment>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<UserEquipment> Users { get; set; }

        public virtual ICollection<OrderEquipment> Orders { get; set; }
    }
}
