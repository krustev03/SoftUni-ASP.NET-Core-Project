namespace FitnessHub.Web.ViewModels.Equipments
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class BaseEquipmentInputModel
    {
        [Required]
        [MinLength(EquipmentNameMinLength, ErrorMessage = ShopItemNameMinLengthErrorMessage)]
        [MaxLength(EquipmentNameMaxLength)]
        [RegularExpression(EquipmentNameRegex, ErrorMessage = CapitalLetterNameRegexErrorMessage)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(EquipmentPriceRegex, ErrorMessage = ShopItemPriceRegexErrorMessage)]
        public string Price { get; set; }

        [Required]
        [MinLength(EquipmentDescriptionMinLength, ErrorMessage = GeneralDescriptionMinLengthErrorMessage)]
        [MaxLength(EquipmentDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
