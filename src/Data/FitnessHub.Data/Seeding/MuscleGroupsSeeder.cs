namespace FitnessHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;

    public class MuscleGroupsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MuscleGroups.Any())
            {
                return;
            }

            await dbContext.MuscleGroups.AddAsync(new MuscleGroup { Name = "Chest" });
            await dbContext.MuscleGroups.AddAsync(new MuscleGroup { Name = "Back" });
            await dbContext.MuscleGroups.AddAsync(new MuscleGroup { Name = "Legs" });
            await dbContext.MuscleGroups.AddAsync(new MuscleGroup { Name = "Biceps" });
            await dbContext.MuscleGroups.AddAsync(new MuscleGroup { Name = "Triceps" });
            await dbContext.MuscleGroups.AddAsync(new MuscleGroup { Name = "Shoulders" });

            await dbContext.SaveChangesAsync();
        }
    }
}
