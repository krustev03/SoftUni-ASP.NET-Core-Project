namespace FitnessHub.Web.ViewModels.TrainerPosts
{
    using System.ComponentModel.DataAnnotations;

    public class TrainerPostInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The name must be at least 2 characters.")]
        [MaxLength(50)]
        [RegularExpression("^[A-Z].*?$", ErrorMessage = "The name must start with capital letter.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The name must be at least 2 characters.")]
        [MaxLength(50)]
        [RegularExpression("^[A-Z].*?$", ErrorMessage = "The name must start with capital letter.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "The description must be at least 20 characters.")]
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
