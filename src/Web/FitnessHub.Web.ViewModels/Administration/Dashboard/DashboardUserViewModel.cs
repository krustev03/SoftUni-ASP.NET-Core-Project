namespace FitnessHub.Web.ViewModels.Administration.Dashboard
{
    using System;
    using System.Collections.Generic;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class DashboardUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<IdentityUserRole<string>> Roles { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
