namespace FitnessHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public class TrainingService : ITrainingService
    {
        private readonly IDeletableEntityRepository<Training> trainingsRepository;
        private readonly IRepository<Exercise> exercisesRepository;

        public TrainingService(
            IDeletableEntityRepository<Training> trainingsRepository,
            IRepository<Exercise> exercisesRepository)
        {
            this.trainingsRepository = trainingsRepository;
            this.exercisesRepository = exercisesRepository;
        }

        public async Task AddTrainingAsync(int programId, string dayOfWeek)
        {
            var training = new Training()
            {
                DayOfWeek = dayOfWeek,
                TrainingProgramId = programId,
            };

            await this.trainingsRepository.AddAsync(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public T GetTrainingDetails<T>(int trainingId)
        {
            var training = this.trainingsRepository.All().Where(x => x.Id == trainingId).To<T>().FirstOrDefault();

            return training;
        }

        public async Task DeleteTrainingByIdAsync(int trainingId)
        {
            var training = this.trainingsRepository.All().Where(x => x.Id == trainingId).FirstOrDefault();
            this.trainingsRepository.Delete(training);
            await this.trainingsRepository.SaveChangesAsync();
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

        public async Task DeleteExerciseByIdAsync(int exerciseId)
        {
            var exercise = this.exercisesRepository.All().Where(x => x.Id == exerciseId).FirstOrDefault();
            this.exercisesRepository.Delete(exercise);
            await this.exercisesRepository.SaveChangesAsync();
        }
    }
}
