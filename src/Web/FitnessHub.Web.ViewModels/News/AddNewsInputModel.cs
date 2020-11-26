namespace FitnessHub.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    public class AddNewsInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "The title must be at least 5 characters.")]
        [MaxLength(40)]
        [RegularExpression("^[A-Z].*?$", ErrorMessage = "The title must start with capital letter.")]
        public string Title { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "The content must be at least 20 characters.")]
        [MaxLength(900)]
        public string Content { get; set; }
    }
}
