namespace FitnessHub.Web.ViewModels.Suplements
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class BaseSuplementInputModel
    {
        [Required]
        [MinLength(SuplementNameMinLength, ErrorMessage = "The name must be at least 5 characters.")]
        [MaxLength(SuplementNameMaxLength)]
        [RegularExpression(SuplementNameRegex, ErrorMessage = "The name must start with capital letter.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(SuplementWeightRegex, ErrorMessage = "The weight must be positive and whole number.")]
        public string Weight { get; set; }

        [Required]
        [RegularExpression(SuplementPriceRegex, ErrorMessage = "The price must be positive.")]
        public string Price { get; set; }

        [Required]
        [MinLength(SuplementDescriptionMinLength, ErrorMessage = "The description must be at least 20 characters.")]
        [MaxLength(SuplementDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
