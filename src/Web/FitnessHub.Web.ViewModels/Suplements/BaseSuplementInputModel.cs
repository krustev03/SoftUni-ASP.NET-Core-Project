namespace FitnessHub.Web.ViewModels.Suplements
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class BaseSuplementInputModel
    {
        [Required]
        [MinLength(SuplementNameMinLength, ErrorMessage = ShopItemNameMinLengthErrorMessage)]
        [MaxLength(SuplementNameMaxLength)]
        [RegularExpression(SuplementNameRegex, ErrorMessage = CapitalLetterNameRegexErrorMessage)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(SuplementWeightRegex, ErrorMessage = SuplementWeightRegexErrorMessage)]
        public string Weight { get; set; }

        [Required]
        [RegularExpression(SuplementPriceRegex, ErrorMessage = ShopItemPriceRegexErrorMessage)]
        public string Price { get; set; }

        [Required]
        [MinLength(SuplementDescriptionMinLength, ErrorMessage = GeneralDescriptionMinLengthErrorMessage)]
        [MaxLength(SuplementDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
