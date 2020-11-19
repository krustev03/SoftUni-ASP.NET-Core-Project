namespace FitnessHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.OrderEquipments = new HashSet<Equipment>();
            this.OrderSuplements = new HashSet<Suplement>();
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

        public virtual ICollection<Equipment> OrderEquipments { get; set; }

        public virtual ICollection<Suplement> OrderSuplements { get; set; }
    }
}
