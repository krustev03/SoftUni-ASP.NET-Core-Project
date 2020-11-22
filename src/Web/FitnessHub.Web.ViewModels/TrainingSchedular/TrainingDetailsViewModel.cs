namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class TrainingDetailsViewModel : IMapFrom<Training>
    {
        public int Id { get; set; }

        public IEnumerable<ExerciseViewModel> Exercises { get; set; }

        public string DayOfWeek { get; set; }
    }
}
