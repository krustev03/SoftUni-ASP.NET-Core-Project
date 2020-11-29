namespace FitnessHub.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class CardValidationModel
    {
        [Required]
        [MinLength(CardNameMinLength, ErrorMessage = "The name is too short.")]
        [RegularExpression(CardNameRegex, ErrorMessage = "This isn't a valid name.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(CardNumberRegex, ErrorMessage = "This isn't a valid card number.")]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression(SecurityCodeRegex, ErrorMessage = "This isn't a valid security code.")]
        public string SecurityCode { get; set; }
    }
}
