namespace FitnessHub.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class ContactsInputModel
    {
        [Required]
        [StringLength(
            45,
            ErrorMessage = "The username must be between 3 and 45 characters.",
            MinimumLength = 3)]
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
            MinimumLength = 5)]
        public string Message { get; set; }
    }
}
