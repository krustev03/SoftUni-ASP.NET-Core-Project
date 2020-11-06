namespace FitnessHub.Web.ViewModels.Suplements
{
    using System.ComponentModel.DataAnnotations;

    public class AddSuplementInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
