﻿namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Suplements;
    using Microsoft.AspNetCore.Identity;

    public class SuplementsService : ISuplementsService
    {
        private readonly IDeletableEntityRepository<Suplement> suplementsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public SuplementsService(
            IDeletableEntityRepository<Suplement> suplementsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.suplementsRepository = suplementsRepository;
            this.userRepository = userRepository;
        }

        public async Task AddSuplementAsync(AddSuplementInputModel suplementInputModel)
        {
            var suplement = new Suplement()
            {
                Name = suplementInputModel.Name,
                Price = decimal.Parse(suplementInputModel.Price, CultureInfo.InvariantCulture),
                Weight = int.Parse(suplementInputModel.Weight, CultureInfo.InvariantCulture),
                Description = suplementInputModel.Description,
                ImageUrl = suplementInputModel.ImageUrl,
            };

            await this.suplementsRepository.AddAsync(suplement);
            await this.suplementsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllSuplements<T>()
        {
            return this.suplementsRepository.All().To<T>();
        }

        public T GetSuplementDetails<T>(int id)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return suplement;
        }

        public async Task DeleteSuplementByIdAsync(int id)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.suplementsRepository.Delete(suplement);
            await this.suplementsRepository.SaveChangesAsync();
        }

        public async Task AddSuplementToCart(int id, string userId)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var appUser = await this.userRepository.GetByIdWithDeletedAsync(userId);
            appUser.Suplements.Add(new UserSuplement
            {
                Suplement = suplement,
            });
            this.userRepository.Update(appUser);
            await this.userRepository.SaveChangesAsync();
            this.suplementsRepository.Update(suplement);
            await this.suplementsRepository.SaveChangesAsync();
        }
    }
}
