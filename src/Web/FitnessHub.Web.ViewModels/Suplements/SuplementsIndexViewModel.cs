namespace FitnessHub.Web.ViewModels.Suplements
{
    using System.Collections.Generic;

    public class SuplementsIndexViewModel : PagingViewModel
    {
        public IEnumerable<SuplementViewModel> Suplements { get; set; }
    }
}
