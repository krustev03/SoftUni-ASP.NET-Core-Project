namespace FitnessHub.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Web.Infrastructure.GoogleReCaptcha;

    using static FitnessHub.Common.GlobalConstants;

    public class ContactsInputModel
    {
        [Required]
        [StringLength(
            ContactsNameInputMaxLength,
            ErrorMessage = "The username must be between 3 and 45 characters.",
            MinimumLength = ContactsNameInputMinLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [StringLength(
            EmailMessageMaxLength,
            ErrorMessage = "The username must be between 3 and 45 characters.",
            MinimumLength = EmailMessageMinLength)]
        public string Message { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
