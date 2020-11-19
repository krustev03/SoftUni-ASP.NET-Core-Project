namespace FitnessHub.Web.ViewModels.Suplements
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class SuplementInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "The name must be at least 5 characters.")]
        [MaxLength(35)]
        [RegularExpression("^[A-Z].*?$", ErrorMessage = "The name must start with capital letter.")]
        public string Name { get; set; }

        [Required]
        public string Weight { get; set; }

        [Required]
        public string Price { get; set; }

        [MaxLength(250)]
        [MinLength(20, ErrorMessage = "The description must be at least 20 characters.")]
        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public string ImageUrl => $"~/suplementsImages/{this.Name}.jpg";
    }
}
