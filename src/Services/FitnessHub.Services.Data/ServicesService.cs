﻿namespace FitnessHub.Services.Data
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

        public ServicesService(
            IDeletableEntityRepository<Service> servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        public async Task AddServiceAsync(ServiceInputModel serviceInputModel, ApplicationUser appUser)
        {
            var service = new Service()
            {
                Name = serviceInputModel.Name,
                Price = decimal.Parse(serviceInputModel.Price, CultureInfo.InvariantCulture),
                Description = serviceInputModel.Description,
                AuthorId = appUser.Id,
            };

            await this.servicesRepository.AddAsync(service);
            await this.servicesRepository.SaveChangesAsync();
        }

        public async Task EditService(int id, ServiceInputModel serviceInputModel)
        {
            var service = this.servicesRepository.All().Where(x => x.Id == id).FirstOrDefault();

            service.Name = serviceInputModel.Name;
            service.Price = decimal.Parse(serviceInputModel.Price, CultureInfo.InvariantCulture);
            service.Description = serviceInputModel.Description;

            this.servicesRepository.Update(service);
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
