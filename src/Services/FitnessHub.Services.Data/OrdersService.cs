namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Orders;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;
        private readonly IRepository<UserEquipment> userEquipmentRepository;
        private readonly IRepository<UserSuplement> userSuplementRepository;
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;
        private readonly IDeletableEntityRepository<Suplement> suplementsRepository;

        public OrdersService(
            IRepository<Order> ordersRepository,
            IRepository<UserEquipment> userEquipmentRepository,
            IRepository<UserSuplement> userSuplementRepository,
            IDeletableEntityRepository<Equipment> equipmentsRepository,
            IDeletableEntityRepository<Suplement> suplementsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.userEquipmentRepository = userEquipmentRepository;
            this.userSuplementRepository = userSuplementRepository;
            this.equipmentsRepository = equipmentsRepository;
            this.suplementsRepository = suplementsRepository;
        }

        public async Task AddOrderAsync(OrderInputModel orderInputModel, ApplicationUser appUser)
        {
            var equipments = await this.GetEquipmentsInOrder(appUser);
            var suplements = await this.GetSuplementsInOrder(appUser);

            var order = new Order()
            {
                FirstName = orderInputModel.FirstName,
                LastName = orderInputModel.LastName,
                BirthDate = orderInputModel.BirthDate,
                City = orderInputModel.City,
                CityCode = orderInputModel.CityCode,
                PhoneNumber = appUser.PhoneNumber,
                Adress = orderInputModel.Adress,
                Price = orderInputModel.Price,
                UserId = appUser.Id,
                OrderEquipments = equipments,
                OrderSuplements = suplements,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            this.RemoveEquipmentsFromUser(appUser);
            this.RemoveSuplementsFromUser(appUser);
        }

        private async Task<List<Equipment>> GetEquipmentsInOrder(ApplicationUser appUser)
        {
            var userEquipments = this.userEquipmentRepository.All().Where(x => x.UserId == appUser.Id).ToList();
            var equipments = new List<Equipment>();
            foreach (var userEquipment in userEquipments)
            {
                var equipment = await this.equipmentsRepository.GetByIdWithDeletedAsync(userEquipment.EquipmentId);

                for (int i = 0; i < userEquipment.Quantity; i++)
                {
                    equipments.Add(equipment);
                }
            }

            return equipments;
        }

        private async Task<List<Suplement>> GetSuplementsInOrder(ApplicationUser appUser)
        {
            var userSuplements = this.userSuplementRepository.All().Where(x => x.UserId == appUser.Id).ToList();
            var suplements = new List<Suplement>();
            foreach (var userSuplement in userSuplements)
            {
                var suplement = await this.suplementsRepository.GetByIdWithDeletedAsync(userSuplement.SuplementId);
                for (int i = 0; i < userSuplement.Quantity; i++)
                {
                    suplements.Add(suplement);
                }
            }

            return suplements;
        }

        private void RemoveEquipmentsFromUser(ApplicationUser appUser)
        {
            var userEquipments = this.userEquipmentRepository.All().Where(x => x.UserId == appUser.Id).ToList();

            foreach (var userEquipment in userEquipments)
            {
                this.userEquipmentRepository.Delete(userEquipment);
            }
        }

        private void RemoveSuplementsFromUser(ApplicationUser appUser)
        {
            var userSuplements = this.userSuplementRepository.All().Where(x => x.UserId == appUser.Id).ToList();

            foreach (var userSuplement in userSuplements)
            {
                this.userSuplementRepository.Delete(userSuplement);
            }
        }
    }
}
