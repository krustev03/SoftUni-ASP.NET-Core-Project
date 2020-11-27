﻿namespace FitnessHub.Web.ViewModels.MyCart
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class SuplementCartViewModel : IMapFrom<Suplement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}
