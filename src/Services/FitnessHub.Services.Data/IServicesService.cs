namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Services;

    public interface IServicesService
    {
        public IEnumerable<T> GetAllServices<T>();

        public Task AddServiceAsync(AddServiceInputModel serviceInputModel, string authorId);
    }
}
