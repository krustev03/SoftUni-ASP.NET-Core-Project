﻿namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.TrainingSchedular;

    public interface ITrainingService
    {
        public Task AddTrainingAsync(int programId, string dayOfWeek);

        public T GetTrainingDetails<T>(int id);

        public Task DeleteTrainingByIdAsync(int trainingId);

        public Task AddExerciseToTrainingAsync(int trainingId, ExerciseInputModel model);

        public Task EditExercise(int id, ExerciseInputModel model);

        public Task DeleteExerciseByIdAsync(int exerciseId);
    }
}