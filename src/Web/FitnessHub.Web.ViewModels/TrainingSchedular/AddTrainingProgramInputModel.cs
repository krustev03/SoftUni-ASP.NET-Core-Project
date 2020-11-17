namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.ComponentModel.DataAnnotations;

    public class AddTrainingProgramInputModel
    {
        [MinLength(3, ErrorMessage = "The program name must be at least 3 characters.")]
        public string Name { get; set; }
    }
}
