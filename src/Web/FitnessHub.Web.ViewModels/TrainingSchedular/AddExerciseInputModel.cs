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

        [Range(1, 30)]
        public int Sets { get; set; }

        [Range(1, 100)]
        public int Reps { get; set; }

        public int MuscleGroupId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MuscleGroupsItems { get; set; }
    }
}
