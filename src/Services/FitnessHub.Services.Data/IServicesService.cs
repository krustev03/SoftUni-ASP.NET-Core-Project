namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Services;

    public interface IServicesService
    {
        public IEnumerable<T> GetAllServices<T>();

        public Task AddServiceAsync(AddServiceInputModel serviceInputModel, string authorId);

        public T GetServiceDetails<T>(int id);

        public Task DeleteServiceByIdAsync(int id);
    }
}
