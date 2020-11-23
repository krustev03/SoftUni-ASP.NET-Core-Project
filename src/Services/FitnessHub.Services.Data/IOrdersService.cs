namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Orders;

    public interface IOrdersService
    {
        public Task AddOrderAsync(OrderInputModel orderInputModel, string userId);
    }
}