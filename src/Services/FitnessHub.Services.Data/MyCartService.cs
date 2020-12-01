namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.MyCart;

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

        public IEnumerable<EquipmentCartViewModel> GetUserEquipments(string userId)
        {
            var equipmentsAsList = new List<EquipmentCartViewModel>();
            var equipments = this.userEquipmentRepository.All().Where(x => x.UserId == userId).ToList();
            foreach (var item in equipments)
            {
                var equipment = this.GetEquipmentById<EquipmentCartViewModel>(item.EquipmentId);
                equipment.Quantity = item.Quantity;
                equipmentsAsList.Add(equipment);
            }

            return equipmentsAsList;
        }

        public IEnumerable<SuplementCartViewModel> GetUserSuplements(string userId)
        {
            var suplementsAsList = new List<SuplementCartViewModel>();
            var suplements = this.userSuplementRepository.All().Where(x => x.UserId == userId).ToList();
            foreach (var item in suplements)
            {
                var suplement = this.GetSuplementById<SuplementCartViewModel>(item.SuplementId);
                suplement.Quantity = item.Quantity;
                suplementsAsList.Add(suplement);
            }

            return suplementsAsList;
        }

        public decimal GetTotalPrice(string userId)
        {
            decimal totalPrice = 0;

            var suplements = this.userSuplementRepository.All().Where(x => x.UserId == userId).ToList();
            var equipments = this.userEquipmentRepository.All().Where(x => x.UserId == userId).ToList();

            foreach (var item in suplements)
            {
                var suplement = this.suplementsRepository.GetByIdWithDeletedAsync(item.SuplementId).Result;
                totalPrice += suplement.Price * item.Quantity;
            }

            foreach (var item in equipments)
            {
                var equipment = this.equipmentsRepository.GetByIdWithDeletedAsync(item.EquipmentId).Result;
                totalPrice += equipment.Price * item.Quantity;
            }

            return totalPrice;
        }

        public async Task RemoveEquipmentFromCartAsync(int id, string userId)
        {
            var userEquipment = this.userEquipmentRepository.All().Where(x => x.EquipmentId == id && x.UserId == userId).FirstOrDefault();

            if (userEquipment.Quantity > 1)
            {
                userEquipment.Quantity--;
            }
            else
            {
                this.userEquipmentRepository.Delete(userEquipment);
            }

            await this.userEquipmentRepository.SaveChangesAsync();
        }

        public async Task RemoveSuplementFromCartAsync(int id, string userId)
        {
            var userSuplement = this.userSuplementRepository.All().Where(x => x.SuplementId == id && x.UserId == userId).FirstOrDefault();

            if (userSuplement.Quantity > 1)
            {
                userSuplement.Quantity--;
            }
            else
            {
                this.userSuplementRepository.Delete(userSuplement);
            }

            await this.userSuplementRepository.SaveChangesAsync();
        }

        private T GetEquipmentById<T>(int equipmentId)
        {
            var equipment = this.equipmentsRepository.AllAsNoTracking()
                .Where(x => x.Id == equipmentId)
                .To<T>().FirstOrDefault();

            return equipment;
        }

        private T GetSuplementById<T>(int suplementId)
        {
            var suplement = this.suplementsRepository.AllAsNoTracking()
                .Where(x => x.Id == suplementId)
                .To<T>().FirstOrDefault();

            return suplement;
        }
    }
}
