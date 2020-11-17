namespace FitnessHub.Data.Models
{
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    public class Training : BaseDeletableModel<int>
    {
        public Training()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        public string DayOfWeek { get; set; }

        public string TrainingProgramId { get; set; }

        public virtual TrainingProgram TrainingProgram { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
