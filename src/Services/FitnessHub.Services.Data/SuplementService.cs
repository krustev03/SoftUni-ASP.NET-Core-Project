namespace FitnessHub.Services.Data
{
    using System;
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

    public class SuplementService : ISuplementService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Suplement> suplementsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserSuplement> userSuplementRepository;
        private readonly IRepository<Image> imagesRepository;

        public SuplementService(
            IDeletableEntityRepository<Suplement> suplementsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserSuplement> userSuplementRepository,
            IRepository<Image> imagesRepository)
        {
            this.suplementsRepository = suplementsRepository;
            this.userRepository = userRepository;
            this.userSuplementRepository = userSuplementRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task AddSuplementAsync(SuplementInputModel model, string userId, string imagePath)
        {
            var suplement = new Suplement()
            {
                Name = model.Name,
                Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture),
                Weight = int.Parse(model.Weight, CultureInfo.InvariantCulture),
                Description = model.Description,
            };

            Directory.CreateDirectory($"{imagePath}/suplements/");

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var dbImage = new Image
            {
                AddedByUserId = userId,
                Extension = extension,
            };
            suplement.Image = dbImage;

            var physicalPath = $"{imagePath}/suplements/{dbImage.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this.suplementsRepository.AddAsync(suplement);
            await this.suplementsRepository.SaveChangesAsync();
        }

        public async Task EditSuplement(SuplementInputModel model, int suplementId, string userId, string imagePath)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == suplementId).FirstOrDefault();

            suplement.Name = model.Name;
            suplement.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
            suplement.Weight = int.Parse(model.Weight, CultureInfo.InvariantCulture);
            suplement.Description = model.Description;

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var dbImage = new Image
            {
                AddedByUserId = userId,
                Extension = extension,
            };

            var image = this.imagesRepository.All().Where(x => x.SuplementId == suplement.Id).FirstOrDefault();
            image.SuplementId = null;

            suplement.ImageId = dbImage.Id;
            suplement.Image = dbImage;
            suplement.Image.SuplementId = suplement.Id;

            var physicalPath = $"{imagePath}/suplements/{suplement.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            this.suplementsRepository.Update(suplement);
            await this.suplementsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        {
            var suplements = this.suplementsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return suplements;
        }

        public IEnumerable<T> GetAllSuplements<T>()
        {
            return this.suplementsRepository.All().To<T>();
        }

        public int GetCount()
        {
            return this.suplementsRepository.All().Count();
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
            var suplementsInCart = this.userSuplementRepository.All().Where(x => x.SuplementId == id).ToList();
            foreach (var item in suplementsInCart)
            {
                this.userSuplementRepository.Delete(item);
            }

            await this.userSuplementRepository.SaveChangesAsync();
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
