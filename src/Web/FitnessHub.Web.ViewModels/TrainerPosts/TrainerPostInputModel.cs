namespace FitnessHub.Web.ViewModels.TrainerPosts
{
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    using static FitnessHub.Common.GlobalConstants;

    public class TrainerPostInputModel : IMapFrom<TrainerPost>
    {
        [Required]
        [MinLength(TrainerPostNameMinLength, ErrorMessage = GeneralNameMinLengthErrorMessage)]
        [MaxLength(TrainerPostNameMaxLength)]
        [RegularExpression(TrainerPostNameRegex, ErrorMessage = CapitalLetterNameRegexErrorMessage)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(TrainerPostNameMinLength, ErrorMessage = GeneralNameMinLengthErrorMessage)]
        [MaxLength(TrainerPostNameMaxLength)]
        [RegularExpression(TrainerPostNameRegex, ErrorMessage = CapitalLetterNameRegexErrorMessage)]
        public string LastName { get; set; }

        [Required]
        [MinLength(TrainerPostDescriptionMinLength, ErrorMessage = GeneralDescriptionMinLengthErrorMessage)]
        [MaxLength(TrainerPostDescriptionMaxLength)]
        public string Description { get; set; }
    }
}
