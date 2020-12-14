namespace FitnessHub.Web.ViewModels.Cart
{
    using AutoMapper;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class SuplementCartViewModel : IMapFrom<Suplement>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Suplement, SuplementCartViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Image.RemoteImageUrl != null ?
                        x.Image.RemoteImageUrl :
                        "/images/suplements/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
