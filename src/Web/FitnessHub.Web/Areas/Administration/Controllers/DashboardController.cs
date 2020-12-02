namespace FitnessHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using FitnessHub.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IUserService userService;

        public DashboardController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            var viewModel = new DashboardIndexViewModel
            {
                Users = this.userService.GetAllUsers<DashboardUserViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
