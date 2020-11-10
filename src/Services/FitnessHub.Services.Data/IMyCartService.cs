namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;

    public interface IMyCartService
    {
        public IEnumerable<Equipment> GetUserEquipments(ApplicationUser appUser);

        public IEnumerable<Suplement> GetUserSuplements(ApplicationUser appUser);
    }
}
