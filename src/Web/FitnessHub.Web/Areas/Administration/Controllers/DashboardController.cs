namespace FitnessHub.Web.Areas.Administration.Controllers
{
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardIndexViewModel
            {
                Users = await this.userManager.Users.ToListAsync(),
            };
            return this.View(viewModel);
        }
    }
}
