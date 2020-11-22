namespace FitnessHub.Data.Models
{
    using FitnessHub.Data.Common.Models;

    public class Exercise : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public int MuscleGroupId { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public int TrainingId { get; set; }

        public virtual Training Training { get; set; }
    }
}
