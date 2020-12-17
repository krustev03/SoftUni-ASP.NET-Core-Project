namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Administration.Dashboard;
    using Xunit;

    public class UserServiceTests : BaseServiceTest
    {
        // Doesn't work
        // IEnumerable<T> GetAllUsers<T>()
        // [Fact] // 1. IEnumerable<T> GetAllUsers<T>()
        // public async void GetAllUsers_ShouldGetAllUsers()
        // {
        //    // Arrange
        //    var userService = new UserService(this.UserManager.Object);

        // var user1 = new ApplicationUser()
        //    {
        //        Id = "24bf72c6-e348-40d1-a7b1-d28dfa033c80",
        //        Email = "pepcho_krastev@abv.bg",
        //        UserName = "Golbarg2000",
        //        PhoneNumber = "0885842694",
        //    };

        // var user2 = new ApplicationUser()
        //    {
        //        Id = "24bf7dd6-e348-40d1-a7b1-d28dfa033c80",
        //        Email = "peps_krastev@abv.bg",
        //        UserName = "Golbarg2",
        //        PhoneNumber = "0875842694",
        //    };

        // await this.UserManager.Object.CreateAsync(user1, "123456");
        //    await this.UserManager.Object.CreateAsync(user2, "123456");

        // // Act
        //    var resultCount = userService.GetAllUsers<DashboardUserViewModel>().Count();
        //    var expectedCount = 2;

        // // Assert
        //    Assert.Equal(expectedCount, resultCount);
        // }
    }
}
