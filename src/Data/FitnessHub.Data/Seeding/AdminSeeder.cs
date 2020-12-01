namespace FitnessHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Common;
    using FitnessHub.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any(x => x.Email == "pepcho_krastev@abv.bg"))
            {
                return;
            }

            var user = new ApplicationUser()
            {
                Email = "pepcho_krastev@abv.bg",
                UserName = "Golbarg2000",
                PhoneNumber = "0885842694",
            };

            await userManager.CreateAsync(user, "123456pepi");
            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
