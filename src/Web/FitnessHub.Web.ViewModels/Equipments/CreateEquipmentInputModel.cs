namespace FitnessHub.Web.ViewModels.Equipments
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateEquipmentInputModel : BaseEquipmentInputModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
