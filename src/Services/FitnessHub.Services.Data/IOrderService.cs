namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Orders;

    public interface IOrderService
    {
        Task AddOrderAsync(OrderInputModel orderInputModel, string userId);
    }
}
