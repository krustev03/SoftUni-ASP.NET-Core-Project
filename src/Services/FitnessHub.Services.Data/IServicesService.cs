namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Services;

    public interface IServicesService
    {
        public IEnumerable<T> GetAllServices<T>();

        public Task AddServiceAsync(ServiceInputModel serviceInputModel, ApplicationUser appUser);

        public Task EditService(int id, ServiceInputModel serviceInputModel);

        public T GetServiceDetails<T>(int id);

        public Task DeleteServiceByIdAsync(int id);
    }
}
