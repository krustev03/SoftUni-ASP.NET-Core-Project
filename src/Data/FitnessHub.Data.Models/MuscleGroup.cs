namespace FitnessHub.Data.Models
{
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    public class MuscleGroup : BaseModel<int>
    {
        public MuscleGroup()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        public string Name { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
