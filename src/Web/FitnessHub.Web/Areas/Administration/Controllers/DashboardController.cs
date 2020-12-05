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
        private readonly IEquipmentService equipmentService;
        private readonly ISuplementService suplementService;
        private readonly INewsService newsService;
        private readonly ITrainerPostService trainerPostService;

        public DashboardController(
            IUserService userService,
            IEquipmentService equipmentService,
            ISuplementService suplementService,
            INewsService newsService,
            ITrainerPostService trainerPostService)
        {
            this.userService = userService;
            this.equipmentService = equipmentService;
            this.suplementService = suplementService;
            this.newsService = newsService;
            this.trainerPostService = trainerPostService;
        }

        public IActionResult Index()
        {
            var viewModel = new DashboardIndexViewModel
            {
                Users = this.userService.GetAllUsers<DashboardUserViewModel>(),
                EquipmentsCount = this.equipmentService.GetCount(),
                SuplementsCount = this.suplementService.GetCount(),
                NewsCount = this.newsService.GetCount(),
                TrainerPostsCount = this.trainerPostService.GetCount(),
            };
            return this.View(viewModel);
        }
    }
}
