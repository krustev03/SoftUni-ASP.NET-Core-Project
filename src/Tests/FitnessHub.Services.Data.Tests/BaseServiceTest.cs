namespace FitnessHub.Services.Data.Tests
{
    using FitnessHub.Data;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data.Tests.Mocks;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Equipments;
    using FitnessHub.Web.ViewModels.MyCart;
    using FitnessHub.Web.ViewModels.News;
    using FitnessHub.Web.ViewModels.Suplements;
    using FitnessHub.Web.ViewModels.TrainerPosts;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    public class BaseServiceTest
    {
        protected ApplicationDbContext Context;
        protected Mock<UserManager<ApplicationUser>> UserManager;

        protected BaseServiceTest()
        {
            this.Context = InMemoryDatabase.Get();
            this.UserManager = UserManagerMock.New;
            AutoMapperConfig.RegisterMappings(
                typeof(EquipmentViewModel).Assembly,
                typeof(EquipmentCartViewModel).Assembly,
                typeof(SuplementViewModel).Assembly,
                typeof(SuplementCartViewModel).Assembly,
                typeof(NewsViewModel).Assembly,
                typeof(TrainerPostViewModel).Assembly);
        }
    }
}
