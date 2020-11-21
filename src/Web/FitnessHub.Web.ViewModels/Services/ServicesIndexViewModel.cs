namespace FitnessHub.Web.ViewModels.Services
{
    using System.Collections.Generic;

    public class ServicesIndexViewModel : PagingViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }
    }
}
