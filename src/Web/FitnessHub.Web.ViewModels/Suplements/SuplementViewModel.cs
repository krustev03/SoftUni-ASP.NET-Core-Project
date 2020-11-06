namespace FitnessHub.Web.ViewModels.Suplements
{
    using System;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class SuplementViewModel : IMapFrom<Suplement>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}