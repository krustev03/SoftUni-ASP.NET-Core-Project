namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddExerciseInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The name must be at least 2 characters")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 30, ErrorMessage = "The sets should be between 1 and 30")]
        public int Sets { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The reps should be between 1 and 100")]
        public int Reps { get; set; }

        public int MuscleGroupId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MuscleGroupsItems { get; set; }
    }
}
