namespace FitnessHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FitnessHub.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.Equipments = new HashSet<OrderEquipment>();
            this.Suplements = new HashSet<OrderSuplement>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string CityCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Adress { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<OrderEquipment> Equipments { get; set; }

        public virtual ICollection<OrderSuplement> Suplements { get; set; }
    }
}
