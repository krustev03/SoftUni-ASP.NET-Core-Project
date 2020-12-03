namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IRepository<UserEquipment> userEquipmentRepository;
        private readonly IRepository<UserSuplement> userSuplementRepository;
        private readonly IRepository<OrderEquipment> orderEquipmentsRepository;
        private readonly IRepository<OrderSuplement> orderSuplementsRepository;
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;
        private readonly IDeletableEntityRepository<Suplement> suplementsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public OrderService(
            IDeletableEntityRepository<Order> ordersRepository,
            IRepository<UserEquipment> userEquipmentRepository,
            IRepository<UserSuplement> userSuplementRepository,
            IRepository<OrderEquipment> orderEquipmentsRepository,
            IRepository<OrderSuplement> orderSuplementsRepository,
            IDeletableEntityRepository<Equipment> equipmentsRepository,
            IDeletableEntityRepository<Suplement> suplementsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.ordersRepository = ordersRepository;
            this.userEquipmentRepository = userEquipmentRepository;
            this.userSuplementRepository = userSuplementRepository;
            this.orderEquipmentsRepository = orderEquipmentsRepository;
            this.orderSuplementsRepository = orderSuplementsRepository;
            this.equipmentsRepository = equipmentsRepository;
            this.suplementsRepository = suplementsRepository;
            this.userRepository = userRepository;
        }

        public async Task AddOrderAsync(OrderInputModel orderInputModel, string userId)
        {
            var appUser = await this.userRepository.GetByIdWithDeletedAsync(userId);

            var order = new Order()
            {
                FirstName = orderInputModel.FirstName,
                LastName = orderInputModel.LastName,
                BirthDate = orderInputModel.BirthDate,
                City = orderInputModel.City,
                CityCode = orderInputModel.CityCode,
                PhoneNumber = appUser.PhoneNumber,
                Adress = orderInputModel.Adress,
                Price = orderInputModel.TotalPrice,
                UserId = appUser.Id,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            var equipments = await this.GetEquipmentsInOrder(appUser.Id);
            await this.AddEquipmentsToOrder(equipments, order);

            var suplements = await this.GetSuplementsInOrder(appUser.Id);
            await this.AddSuplementsToOrder(suplements, order);

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();

            this.RemoveEquipmentsFromUser(appUser.Id);
            this.RemoveSuplementsFromUser(appUser.Id);

            appUser.Orders.Add(order);
            this.userRepository.Update(appUser);
            await this.userRepository.SaveChangesAsync();
        }

        private async Task AddEquipmentsToOrder(List<Equipment> equipments, Order order)
        {
            foreach (var equipment in equipments)
            {
                var orderEquipment = this.orderEquipmentsRepository.All().Where(x => x.EquipmentId == equipment.Id && x.OrderId == order.Id).FirstOrDefault();

                if (orderEquipment == null)
                {
                    order.Equipments.Add(new OrderEquipment
                    {
                        Equipment = equipment,
                    });
                    this.ordersRepository.Update(order);
                    await this.ordersRepository.SaveChangesAsync();
                    this.equipmentsRepository.Update(equipment);
                    await this.equipmentsRepository.SaveChangesAsync();
                }
                else
                {
                    orderEquipment.Quantity++;
                    this.orderEquipmentsRepository.Update(orderEquipment);
                    await this.orderEquipmentsRepository.SaveChangesAsync();
                }
            }
        }

        private async Task AddSuplementsToOrder(List<Suplement> suplements, Order order)
        {
            foreach (var suplement in suplements)
            {
                var orderSuplement = this.orderSuplementsRepository.All().Where(x => x.SuplementId == suplement.Id && x.OrderId == order.Id).FirstOrDefault();

                if (orderSuplement == null)
                {
                    order.Suplements.Add(new OrderSuplement
                    {
                        Suplement = suplement,
                    });
                    this.ordersRepository.Update(order);
                    await this.ordersRepository.SaveChangesAsync();
                    this.suplementsRepository.Update(suplement);
                    await this.suplementsRepository.SaveChangesAsync();
                }
                else
                {
                    orderSuplement.Quantity++;
                    this.orderSuplementsRepository.Update(orderSuplement);
                    await this.orderSuplementsRepository.SaveChangesAsync();
                }
            }
        }

        private async Task<List<Equipment>> GetEquipmentsInOrder(string userId)
        {
            var userEquipments = this.userEquipmentRepository.All().Where(x => x.UserId == userId).ToList();
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

        private async Task<List<Suplement>> GetSuplementsInOrder(string userId)
        {
            var userSuplements = this.userSuplementRepository.All().Where(x => x.UserId == userId).ToList();
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

        private void RemoveEquipmentsFromUser(string userId)
        {
            var userEquipments = this.userEquipmentRepository.All().Where(x => x.UserId == userId).ToList();

            foreach (var userEquipment in userEquipments)
            {
                this.userEquipmentRepository.Delete(userEquipment);
            }
        }

        private void RemoveSuplementsFromUser(string userId)
        {
            var userSuplements = this.userSuplementRepository.All().Where(x => x.UserId == userId).ToList();

            foreach (var userSuplement in userSuplements)
            {
                this.userSuplementRepository.Delete(userSuplement);
            }
        }
    }
}
