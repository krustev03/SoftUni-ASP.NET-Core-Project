namespace FitnessHub.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class CreditCardInputValidationModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The name is too short.")]
        //[RegularExpression("^[a-zA-Z]+(?:[\\s.]+[a-zA-Z]+)*$", ErrorMessage = "This isn't a valid name.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "This isn't a valid card number.")]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "This isn't a valid security code.")]
        public string SecurityCode { get; set; }
    }
}
