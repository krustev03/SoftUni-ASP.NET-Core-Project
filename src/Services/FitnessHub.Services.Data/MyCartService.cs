namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;

    public class MyCartService : IMyCartService
    {
        public IEnumerable<Equipment> GetUserEquipments(ApplicationUser appUser)
        {
            var equipments = appUser.Equipments;

            return equipments;
        }

        public IEnumerable<Suplement> GetUserSuplements(ApplicationUser appUser)
        {
            var suplements = appUser.Suplements;

            return suplements;
        }
    }
}
