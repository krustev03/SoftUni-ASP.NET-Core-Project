namespace FitnessHub.Web.ViewModels.Suplements
{
    using System;

    using AutoMapper;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class SuplementViewModel : IMapFrom<Suplement>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Suplement, SuplementViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Image.RemoteImageUrl != null ?
                        x.Image.RemoteImageUrl :
                        "/images/suplements/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
