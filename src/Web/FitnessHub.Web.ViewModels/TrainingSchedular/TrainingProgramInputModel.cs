namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.ComponentModel.DataAnnotations;

    using static FitnessHub.Common.GlobalConstants;

    public class TrainingProgramInputModel
    {
        [Required]
        [MinLength(TrainingProgramNameMinLength, ErrorMessage = "The program name must be at least 3 characters.")]
        [MaxLength(TrainingProgramNameMaxLength)]
        public string Name { get; set; }
    }
}
