namespace FitnessHub.Web.ViewModels.Chat
{
    using System.ComponentModel.DataAnnotations;

    public class AddMessageInputModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "You cannot send empty messages.")]
        [MaxLength(1500)]
        public string Content { get; set; }
    }
}
