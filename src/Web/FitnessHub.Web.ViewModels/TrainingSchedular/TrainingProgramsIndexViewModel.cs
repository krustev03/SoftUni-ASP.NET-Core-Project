namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.Collections.Generic;

    public class TrainingProgramsIndexViewModel : PagingViewModel
    {
        public IEnumerable<TrainingProgramViewModel> TrainingPrograms { get; set; }
    }
}
