﻿namespace FitnessHub.Web.ViewModels.TrainingSchedular
{
    using System.ComponentModel.DataAnnotations;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;

    using static FitnessHub.Common.GlobalConstants;

    public class TrainingProgramInputModel : IMapFrom<TrainingProgram>
    {
        [Required]
        [MinLength(TrainingProgramNameMinLength, ErrorMessage = TrainingProgramNameMinLengthErrorMessage)]
        [MaxLength(TrainingProgramNameMaxLength)]
        public string Name { get; set; }
    }
}
