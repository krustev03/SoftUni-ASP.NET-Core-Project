namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    public class TrainingViewModel : IMapFrom<Training>
    {
        public IEnumerable<TrainingViewModel> Exercises { get; set; }

        public string DayOfWeek { get; set; }
    }
}
