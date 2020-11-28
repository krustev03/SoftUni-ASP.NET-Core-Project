namespace FitnessHub.Web.ViewModels.MyCart
{
    using AutoMapper;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class EquipmentCartViewModel : IMapFrom<Equipment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Equipment, EquipmentCartViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Image.RemoteImageUrl != null ?
                        x.Image.RemoteImageUrl :
                        "/images/equipments/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
