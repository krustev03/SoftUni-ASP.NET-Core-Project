namespace FitnessHub.Services.Data
{
    using System.Globalization;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Orders;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;

        public OrdersService(IRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task AddOrderAsync(OrderInputModel orderInputModel, ApplicationUser appUser)
        {
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
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
