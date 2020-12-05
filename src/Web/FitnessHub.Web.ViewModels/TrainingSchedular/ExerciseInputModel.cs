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
        [MinLength(ExerciseNameMinLength, ErrorMessage = "The name must be at least 2 characters")]
        [MaxLength(ExerciseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(ExerciseSetsMinLength, ExerciseSetsMaxLength, ErrorMessage = "The sets should be between 1 and 30")]
        public int Sets { get; set; }

        [Required]
        [Range(ExerciseRepsMinLength, ExerciseRepsMaxLength, ErrorMessage = "The reps should be between 1 and 100")]
        public int Reps { get; set; }

        public int MuscleGroupId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MuscleGroupsItems { get; set; }
    }
}
