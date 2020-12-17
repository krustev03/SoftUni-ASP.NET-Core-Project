namespace FitnessHub.Services.Data.Tests
{
    using FitnessHub.Data;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data.Tests.Mocks;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Administration.Dashboard;
    using FitnessHub.Web.ViewModels.Cart;
    using FitnessHub.Web.ViewModels.Equipments;
    using FitnessHub.Web.ViewModels.News;
    using FitnessHub.Web.ViewModels.Suplements;
    using FitnessHub.Web.ViewModels.TrainerPosts;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    public class BaseServiceTest
    {
        protected ApplicationDbContext context;
        protected Mock<UserManager<ApplicationUser>> userManager;

        protected BaseServiceTest()
        {
            this.context = InMemoryDatabase.Get();
            this.userManager = UserManagerMock.New;
            AutoMapperConfig.RegisterMappings(
                typeof(EquipmentViewModel).Assembly,
                typeof(EquipmentCartViewModel).Assembly,
                typeof(SuplementViewModel).Assembly,
                typeof(SuplementCartViewModel).Assembly,
                typeof(NewsViewModel).Assembly,
                typeof(TrainerPostViewModel).Assembly,
                typeof(TrainingProgramViewModel).Assembly,
                typeof(TrainingDetailsViewModel).Assembly,
                typeof(ExerciseViewModel).Assembly,
                typeof(DashboardUserViewModel).Assembly,
                typeof(DashboardUserViewModel).Assembly);
        }
    }
}
