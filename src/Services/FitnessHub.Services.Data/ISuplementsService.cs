﻿namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Suplements;

    public interface ISuplementsService
    {
        public IEnumerable<T> GetAllSuplements<T>();

        public Task AddSuplementAsync(AddSuplementInputModel suplementInputModel, ApplicationUser appUser);

        public T GetSuplementDetails<T>(int id);

        public Task DeleteSuplementByIdAsync(int id);
    }
}
