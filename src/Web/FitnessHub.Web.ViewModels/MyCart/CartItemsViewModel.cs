namespace FitnessHub.Web.ViewModels.MyCart
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;

    public class CartItemsViewModel
    {
        public IEnumerable<Equipment> Equipments { get; set; }

        public IEnumerable<Suplement> Suplements { get; set; }
    }
}
