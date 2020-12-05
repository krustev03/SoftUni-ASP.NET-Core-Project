namespace FitnessHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;

    public class DashboardIndexViewModel
    {
        public IEnumerable<DashboardUserViewModel> Users { get; set; }

        public int EquipmentsCount { get; set; }

        public int SuplementsCount { get; set; }

        public int NewsCount { get; set; }

        public int TrainerPostsCount { get; set; }

    }
}
