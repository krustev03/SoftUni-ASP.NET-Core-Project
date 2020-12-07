namespace FitnessHub.Web.ViewModels.Equipments
{
    using System.Collections.Generic;

    public class EquipmentsIndexViewModel : PagingViewModel
    {
        public IEnumerable<EquipmentViewModel> Equipments { get; set; }
    }
}
