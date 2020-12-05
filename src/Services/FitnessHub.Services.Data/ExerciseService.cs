namespace FitnessHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public class ExerciseService : IExerciseService
    {
        private readonly IDeletableEntityRepository<Training> trainingsRepository;
        private readonly IDeletableEntityRepository<Exercise> exercisesRepository;

        public ExerciseService(
            IDeletableEntityRepository<Exercise> exercisesRepository,
            IDeletableEntityRepository<Training> trainingsRepository)
        {
            this.exercisesRepository = exercisesRepository;
            this.trainingsRepository = trainingsRepository;
        }

        public async Task AddExerciseToTrainingAsync(int trainingId, ExerciseInputModel model)
        {
            var exercise = new Exercise()
            {
                Name = model.Name,
                Sets = model.Sets,
                Reps = model.Reps,
                MuscleGroupId = model.MuscleGroupId,
                TrainingId = trainingId,
            };

            await this.exercisesRepository.AddAsync(exercise);
            await this.exercisesRepository.SaveChangesAsync();

            var training = await this.trainingsRepository.GetByIdWithDeletedAsync(trainingId);
            training.Exercises.Add(exercise);
            this.trainingsRepository.Update(training);
        }

        public async Task EditExercise(int exerciseId, ExerciseInputModel model)
        {
            var exercise = this.exercisesRepository.All().Where(x => x.Id == exerciseId).FirstOrDefault();

            exercise.Name = model.Name;
            exercise.Sets = model.Sets;
            exercise.Reps = model.Reps;
            exercise.MuscleGroupId = model.MuscleGroupId;

            this.exercisesRepository.Update(exercise);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public T GetExerciseById<T>(int exerciseId)
        {
            var exercise = this.exercisesRepository.All()
                .Where(x => x.Id == exerciseId)
                .To<T>().FirstOrDefault();
            return exercise;
        }

        public async Task DeleteExerciseByIdAsync(int exerciseId)
        {
            var exercise = this.exercisesRepository.All().Where(x => x.Id == exerciseId).FirstOrDefault();
            this.exercisesRepository.Delete(exercise);
            await this.exercisesRepository.SaveChangesAsync();
        }
    }
}
