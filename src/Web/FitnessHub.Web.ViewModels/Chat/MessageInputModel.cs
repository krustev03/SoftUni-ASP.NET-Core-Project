namespace FitnessHub.Web.ViewModels.Chat
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class MessageInputModel
    {
        [Required]
        [MinLength(ChatMessageMinLength, ErrorMessage = ChatMessageMinLengthErrorMessage)]
        [MaxLength(ChatMessageMaxLength)]
        public string Content { get; set; }
    }
}
