namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Services;

    public class ServicesService : IServicesService
    {
        private readonly IDeletableEntityRepository<Service> servicesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ServicesService(
            IDeletableEntityRepository<Service> servicesRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.servicesRepository = servicesRepository;
            this.userRepository = userRepository;
        }

        public async Task AddServiceAsync(AddServiceInputModel serviceInputModel, string authorId)
        {
            var service = new Service()
            {
                Name = serviceInputModel.Name,
                Price = decimal.Parse(serviceInputModel.Price, CultureInfo.InvariantCulture),
                Description = serviceInputModel.Description,
                AuthorId = authorId,
            };

            await this.servicesRepository.AddAsync(service);
            await this.servicesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllServices<T>()
        {
            return this.servicesRepository.All().To<T>();
        }

        public T GetServiceDetails<T>(int id)
        {
            var service = this.servicesRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return service;
        }

        public async Task DeleteServiceByIdAsync(int id)
        {
            var service = this.servicesRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.servicesRepository.Delete(service);
            await this.servicesRepository.SaveChangesAsync();
        }
    }
}
