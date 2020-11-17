namespace FitnessHub.Data.Models
{
    using System.Collections.Generic;

    using FitnessHub.Data.Common.Models;

    public class TrainingProgram : BaseDeletableModel<int>
    {
        public TrainingProgram()
        {
            this.Trainings = new HashSet<Training>();
        }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }
}
