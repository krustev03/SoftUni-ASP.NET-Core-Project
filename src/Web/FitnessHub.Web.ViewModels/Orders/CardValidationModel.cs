namespace FitnessHub.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class CardValidationModel
    {
        [Required]
        [MinLength(CardNameMinLength, ErrorMessage = CardNameMinLengthErrorMessage)]
        [RegularExpression(CardNameRegex, ErrorMessage = CardNameRegexErrorMessage)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(CardNumberRegex, ErrorMessage = CardNumberRegexErrorMessage)]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression(SecurityCodeRegex, ErrorMessage = SecurityCodeRegexErrorMessage)]
        public string SecurityCode { get; set; }
    }
}
