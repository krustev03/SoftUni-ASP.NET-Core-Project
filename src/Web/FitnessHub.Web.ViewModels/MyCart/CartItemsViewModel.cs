﻿namespace FitnessHub.Web.ViewModels.MyCart
{
    using System.Collections.Generic;

    public class CartItemsViewModel
    {
        public IEnumerable<EquipmentCartViewModel> Equipments { get; set; }

        public IEnumerable<SuplementCartViewModel> Suplements { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
