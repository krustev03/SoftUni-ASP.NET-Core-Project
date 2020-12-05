namespace FitnessHub.Web.ViewModels.Suplements
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateSuplementInputModel : BaseSuplementInputModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
