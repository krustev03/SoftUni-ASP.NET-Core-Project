namespace FitnessHub.Web.ViewModels.Services
{
    using System.ComponentModel.DataAnnotations;

    public class AddServiceInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "The name must be at least 5 characters.")]
        [MaxLength(20)]
        [RegularExpression("^[A-Z].*?$", ErrorMessage = "The name must start with capital letter.")]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
