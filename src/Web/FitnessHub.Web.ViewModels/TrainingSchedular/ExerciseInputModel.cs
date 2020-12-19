namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    using static FitnessHub.Common.GlobalConstants;

    public class ExerciseInputModel : IMapFrom<Exercise>
    {
        [Required]
        [MinLength(ExerciseNameMinLength, ErrorMessage = GeneralNameMinLengthErrorMessage)]
        [MaxLength(ExerciseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(ExerciseSetsMinLength, ExerciseSetsMaxLength, ErrorMessage = ExerciseSetsLengthErrorMessage)]
        public int Sets { get; set; }

        [Required]
        [Range(ExerciseRepsMinLength, ExerciseRepsMaxLength, ErrorMessage = ExerciseRepsLengthErrorMessage)]
        public int Reps { get; set; }

        public int MuscleGroupId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MuscleGroupsItems { get; set; }
    }
}
