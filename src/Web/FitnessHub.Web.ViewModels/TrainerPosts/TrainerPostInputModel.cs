namespace FitnessHub.Web.ViewModels.TrainerPosts
{
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    using static FitnessHub.Common.GlobalConstants;

    public class TrainerPostInputModel : IMapFrom<TrainerPost>
    {
        [Required]
        [MinLength(TrainerPostNameMinLength, ErrorMessage = "The first name must be at least 2 characters.")]
        [MaxLength(TrainerPostNameMaxLength)]
        [RegularExpression(TrainerPostNameRegex, ErrorMessage = "The first name must start with capital letter.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(TrainerPostNameMinLength, ErrorMessage = "The last name must be at least 2 characters.")]
        [MaxLength(TrainerPostNameMaxLength)]
        [RegularExpression(TrainerPostNameRegex, ErrorMessage = "The last name must start with capital letter.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(TrainerPostDescriptionMinLength, ErrorMessage = "The description must be at least 20 characters.")]
        [MaxLength(TrainerPostDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
