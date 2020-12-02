namespace FitnessHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;

    public class DashboardIndexViewModel
    {
        public IEnumerable<DashboardUserViewModel> Users { get; set; }
    }
}
