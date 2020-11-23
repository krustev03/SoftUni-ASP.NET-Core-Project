namespace FitnessHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;

    public class DashboardIndexViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
