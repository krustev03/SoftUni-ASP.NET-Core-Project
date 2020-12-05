namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.TrainingSchedular;
    using Xunit;

    public class ExerciseServiceTests : BaseServiceTest
    {
        // async Task AddExerciseToTrainingAsync(int trainingId, ExerciseInputModel model)

        // async Task EditExercise(int exerciseId, ExerciseInputModel model)

        // T GetExerciseById<T>(int exerciseId)

        // async Task DeleteExerciseByIdAsync(int exerciseId)
        [Fact] // 1. async Task AddExerciseToTrainingAsync(int trainingId, ExerciseInputModel model)
        public async void AddExerciseToTrainingAsync_ShouldAddExerciseToTrainingAndInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.Context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);
            var exercisesRepository = new EfDeletableEntityRepository<Exercise>(this.Context);
            var exerciseService = new ExerciseService(exercisesRepository, trainingsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingService.AddTrainingAsync(1, "Monday");

            var exerciseModel = new ExerciseInputModel()
            {
                Name = "Push-ups",
                Sets = 3,
                Reps = 2,
                MuscleGroupId = 2,
            };

            // Act
            await exerciseService.AddExerciseToTrainingAsync(1, exerciseModel);
            var exercise = await exercisesRepository.GetByIdWithDeletedAsync(1);
            var expectedName = "Push-ups";
            var expectedSets = 3;
            var expectedReps = 2;
            var expectedMuscleGroupId = 2;

            // Assert
            Assert.Equal(expectedName, exercise.Name);
            Assert.Equal(expectedSets, exercise.Sets);
            Assert.Equal(expectedReps, exercise.Reps);
            Assert.Equal(expectedMuscleGroupId, exercise.MuscleGroupId);
        }

        [Fact] // 2. async Task EditExercise(int exerciseId, ExerciseInputModel model)
        public async void EditExercise_ShouldEditTheExerciseWithTheGivenId()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.Context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);
            var exercisesRepository = new EfDeletableEntityRepository<Exercise>(this.Context);
            var exerciseService = new ExerciseService(exercisesRepository, trainingsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingService.AddTrainingAsync(1, "Monday");

            var exerciseModel1 = new ExerciseInputModel()
            {
                Name = "Push-ups",
                Sets = 3,
                Reps = 2,
                MuscleGroupId = 2,
            };

            var exerciseModel2 = new ExerciseInputModel()
            {
                Name = "Chin-ups",
                Sets = 4,
                Reps = 4,
                MuscleGroupId = 3,
            };

            await exerciseService.AddExerciseToTrainingAsync(1, exerciseModel1);

            // Act
            await exerciseService.EditExercise(1, exerciseModel2);
            var exercise = await exercisesRepository.GetByIdWithDeletedAsync(1);
            var expectedName = "Chin-ups";
            var expectedSets = 4;
            var expectedReps = 4;
            var expectedMuscleGroupId = 3;

            // Assert
            Assert.Equal(expectedName, exercise.Name);
            Assert.Equal(expectedSets, exercise.Sets);
            Assert.Equal(expectedReps, exercise.Reps);
            Assert.Equal(expectedMuscleGroupId, exercise.MuscleGroupId);
        }

        [Fact] // 3. T GetExerciseById<T>(int exerciseId)
        public async void GetExerciseById_ShouldGetExerciseByIdInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.Context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);
            var exercisesRepository = new EfDeletableEntityRepository<Exercise>(this.Context);
            var exerciseService = new ExerciseService(exercisesRepository, trainingsRepository);
            var muscleGrousRepository = new EfRepository<MuscleGroup>(this.Context);
            var muscleGroupService = new MuscleGroupService(muscleGrousRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingService.AddTrainingAsync(1, "Monday");

            var exerciseModel = new ExerciseInputModel()
            {
                Name = "Push-ups",
                Sets = 3,
                Reps = 2,
                MuscleGroupId = 2,
            };

            await exerciseService.AddExerciseToTrainingAsync(1, exerciseModel);

            // Act
            var exercise = exerciseService.GetExerciseById<ExerciseInputModel>(1);
            exercise.MuscleGroupsItems = muscleGroupService.GetAllAsKeyValuePairs();
            var expectedName = "Push-ups";
            var expectedSets = 3;
            var expectedReps = 2;
            var expectedMuscleGroupId = 2;

            // Assert
            Assert.Equal(expectedName, exercise.Name);
            Assert.Equal(expectedSets, exercise.Sets);
            Assert.Equal(expectedReps, exercise.Reps);
            Assert.Equal(expectedMuscleGroupId, exercise.MuscleGroupId);
        }

        [Fact] // 4. async Task DeleteExerciseByIdAsync(int exerciseId)
        public async void DeleteExerciseByIdAsync_ShouldDeleteExerciseFromTrainingAndInDatabase()
        {
            // Arrange
            var trainingProgramsRepository = new EfDeletableEntityRepository<TrainingProgram>(this.Context);
            var trainingProgramService = new TrainingProgramService(trainingProgramsRepository);
            var trainingsRepository = new EfDeletableEntityRepository<Training>(this.Context);
            var trainingService = new TrainingService(trainingsRepository, trainingProgramsRepository);
            var exercisesRepository = new EfDeletableEntityRepository<Exercise>(this.Context);
            var exerciseService = new ExerciseService(exercisesRepository, trainingsRepository);

            var model1 = new TrainingProgramInputModel()
            {
                Name = "Programa",
            };

            await trainingProgramService.AddTrainingProgramAsync(model1, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            await trainingService.AddTrainingAsync(1, "Monday");

            var exerciseModel = new ExerciseInputModel()
            {
                Name = "Push-ups",
                Sets = 3,
                Reps = 2,
                MuscleGroupId = 2,
            };

            await exerciseService.AddExerciseToTrainingAsync(1, exerciseModel);

            // Act
            await exerciseService.DeleteExerciseByIdAsync(1);
            var resultCount = exercisesRepository.All().ToList().Count();
            var expectedCount = 0;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
