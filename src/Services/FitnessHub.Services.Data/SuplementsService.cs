namespace FitnessHub.Services.Data
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
        private readonly IRepository<UserSuplement> userSuplementRepository;

        public SuplementsService(
            IDeletableEntityRepository<Suplement> suplementsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserSuplement> userSuplementRepository)
        {
            this.suplementsRepository = suplementsRepository;
            this.userRepository = userRepository;
            this.userSuplementRepository = userSuplementRepository;
        }

        public async Task AddSuplementAsync(SuplementInputModel suplementInputModel)
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

        public async Task EditSuplement(int id, SuplementInputModel suplementInputModel)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            suplement.Name = suplementInputModel.Name;
            suplement.Price = decimal.Parse(suplementInputModel.Price, CultureInfo.InvariantCulture);
            suplement.Weight = int.Parse(suplementInputModel.Weight, CultureInfo.InvariantCulture);
            suplement.Description = suplementInputModel.Description;
            suplement.ImageUrl = suplementInputModel.ImageUrl;

            this.suplementsRepository.Update(suplement);
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
            var userSuplement = this.userSuplementRepository.All().Where(x => x.SuplementId == suplement.Id && x.UserId == userId).FirstOrDefault();

            if (userSuplement == null)
            {
                appUser.Suplements.Add(new UserSuplement
                {
                    Suplement = suplement,
                });
                this.userRepository.Update(appUser);
                await this.userRepository.SaveChangesAsync();
                this.suplementsRepository.Update(suplement);
                await this.suplementsRepository.SaveChangesAsync();
            }
            else
            {
                userSuplement.Quantity++;
                this.userSuplementRepository.Update(userSuplement);
                await this.userSuplementRepository.SaveChangesAsync();
            }
        }
    }
}
