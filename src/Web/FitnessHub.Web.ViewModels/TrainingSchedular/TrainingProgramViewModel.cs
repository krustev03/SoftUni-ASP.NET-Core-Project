namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System;
    using System.Collections.Generic;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class TrainingProgramViewModel : IMapFrom<TrainingProgram>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
