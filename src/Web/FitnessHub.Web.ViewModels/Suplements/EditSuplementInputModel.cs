namespace FitnessHub.Web.ViewModels.Suplements
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class EditSuplementInputModel : BaseSuplementInputModel, IMapFrom<Suplement>
    {
        public int Id { get; set; }
    }
}
