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

        public async Task AddSuplementAsync(CreateSuplementInputModel model, string userId, string imagePath)
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

        public async Task EditSuplement(EditSuplementInputModel model, int suplementId, string userId)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == suplementId).FirstOrDefault();

            suplement.Name = model.Name;
            suplement.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
            suplement.Weight = int.Parse(model.Weight, CultureInfo.InvariantCulture);
            suplement.Description = model.Description;

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

        public IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)
        {
            return this.suplementsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public T GetSuplementById<T>(int suplementId)
        {
            var suplement = this.suplementsRepository.AllAsNoTracking()
                .Where(x => x.Id == suplementId)
                .To<T>().FirstOrDefault();
            return suplement;
        }

        public int GetCount()
        {
            return this.suplementsRepository.All().Count();
        }

        public int GetFilteredCount(string searchString)
        {
            return this.suplementsRepository.All()
                .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
                .Count();
        }

        public async Task DeleteSuplementByIdAsync(int suplementId)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == suplementId).FirstOrDefault();
            this.suplementsRepository.Delete(suplement);
            var suplementsInCart = this.userSuplementRepository.All().Where(x => x.SuplementId == suplementId).ToList();
            foreach (var item in suplementsInCart)
            {
                this.userSuplementRepository.Delete(item);
            }

            await this.userSuplementRepository.SaveChangesAsync();
            await this.suplementsRepository.SaveChangesAsync();
        }

        public async Task AddSuplementToCart(int suplementId, string userId)
        {
            var suplement = this.suplementsRepository.All().Where(x => x.Id == suplementId).FirstOrDefault();
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
