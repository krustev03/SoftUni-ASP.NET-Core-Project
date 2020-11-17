namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class ExerciseViewModel : IMapFrom<Exercise>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public string MuscleGroup { get; set; }
    }
}
