namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;

    public class MyCartService : IMyCartService
    {
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;
        private readonly IDeletableEntityRepository<Suplement> suplementsRepository;
        private readonly IRepository<UserEquipment> userEquipmentRepository;
        private readonly IRepository<UserSuplement> userSuplementRepository;

        public MyCartService(
            IDeletableEntityRepository<Equipment> equipmentsRepository,
            IDeletableEntityRepository<Suplement> suplementsRepository,
            IRepository<UserEquipment> userEquipmentRepository,
            IRepository<UserSuplement> userSuplementRepository)
        {
            this.equipmentsRepository = equipmentsRepository;
            this.suplementsRepository = suplementsRepository;
            this.userEquipmentRepository = userEquipmentRepository;
            this.userSuplementRepository = userSuplementRepository;
        }

        public IEnumerable<Equipment> GetUserEquipments(ApplicationUser appUser)
        {
            var equipmentsAsList = new List<Equipment>();
            var equipments = this.userEquipmentRepository.All().Where(x => x.UserId == appUser.Id).ToList();
            foreach (var item in equipments)
            {
                var equipment = this.equipmentsRepository.GetByIdWithDeletedAsync(item.EquipmentId).Result;
                equipmentsAsList.Add(equipment);
            }

            return equipmentsAsList;
        }

        public IEnumerable<Suplement> GetUserSuplements(ApplicationUser appUser)
        {
            var suplementsAsList = new List<Suplement>();
            var suplements = this.userSuplementRepository.All().Where(x => x.UserId == appUser.Id).ToList();
            foreach (var item in suplements)
            {
                var suplement = this.suplementsRepository.GetByIdWithDeletedAsync(item.SuplementId).Result;
                suplementsAsList.Add(suplement);
            }

            return suplementsAsList;
        }
    }
}
