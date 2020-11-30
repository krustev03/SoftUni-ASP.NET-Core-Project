namespace FitnessHub.Services.Data.Tests
{
    using FitnessHub.Data;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data.Tests.Mocks;
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
        }
    }
}
