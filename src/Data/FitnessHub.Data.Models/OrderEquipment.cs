using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessHub.Data.Models
{
    public class OrderEquipment
    {
        public int Id { get; set; }

        public virtual Order Order { get; set; }

        public string UserId { get; set; }

        public int EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
