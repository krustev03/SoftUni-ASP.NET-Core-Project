namespace FitnessHub.Web.ViewModels.Equipments
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class BaseEquipmentInputModel
    {
        [Required]
        [MinLength(EquipmentNameMinLength, ErrorMessage = "The name must be at least 5 characters.")]
        [MaxLength(EquipmentNameMaxLength)]
        [RegularExpression(EquipmentNameRegex, ErrorMessage = "The name must start with capital letter.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(EquipmentPriceRegex, ErrorMessage = "The price must be positive.")]
        public string Price { get; set; }

        [Required]
        [MinLength(EquipmentDescriptionMinLength, ErrorMessage = "The description must be at least 20 characters.")]
        [MaxLength(EquipmentDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
