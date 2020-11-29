namespace FitnessHub.Web.ViewModels.Chat
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class MessageInputModel
    {
        [Required]
        [MinLength(ChatMessageMinLength, ErrorMessage = "You cannot send empty messages.")]
        [MaxLength(ChatMessageMaxLength)]
        public string Content { get; set; }
    }
}
