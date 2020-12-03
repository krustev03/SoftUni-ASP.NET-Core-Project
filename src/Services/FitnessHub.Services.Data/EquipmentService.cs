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
    using FitnessHub.Web.ViewModels.Equipments;

    public class EquipmentService : IEquipmentService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserEquipment> userEquipmentRepository;
        private readonly IRepository<Image> imagesRepository;

        public EquipmentService(
            IDeletableEntityRepository<Equipment> equipmentsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserEquipment> userEquipmentRepository,
            IRepository<Image> imagesRepository)
        {
            this.equipmentsRepository = equipmentsRepository;
            this.userRepository = userRepository;
            this.userEquipmentRepository = userEquipmentRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task AddEquipmentAsync(EquipmentInputModel model, string userId, string imagePath)
        {
            var equipment = new Equipment()
            {
                Name = model.Name,
                Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture),
                Description = model.Description,
            };

            Directory.CreateDirectory($"{imagePath}/equipments/");

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
            equipment.Image = dbImage;

            var physicalPath = $"{imagePath}/equipments/{dbImage.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this.equipmentsRepository.AddAsync(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
        }

        public async Task EditEquipment(EquipmentInputModel model, int equipmentId, string userId, string imagePath)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == equipmentId).FirstOrDefault();

            equipment.Name = model.Name;
            equipment.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
            equipment.Description = model.Description;

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var image = this.imagesRepository.All().Where(x => x.EquipmentId == equipment.Id).FirstOrDefault();
            image.AddedByUserId = userId;
            image.Extension = extension;

            equipment.Image = image;

            var physicalPath = $"{imagePath}/equipments/{equipment.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            this.equipmentsRepository.Update(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
            this.imagesRepository.Update(image);
            await this.imagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        {
            var equipments = this.equipmentsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return equipments;
        }

        public int GetCount()
        {
            return this.equipmentsRepository.All().Count();
        }

        public async Task AddEquipmentToCart(int equipmentId, string userId)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == equipmentId).FirstOrDefault();
            var appUser = await this.userRepository.GetByIdWithDeletedAsync(userId);
            var userEquipment = this.userEquipmentRepository.All().Where(x => x.EquipmentId == equipment.Id && x.UserId == userId).FirstOrDefault();

            if (userEquipment == null)
            {
                appUser.Equipments.Add(new UserEquipment
                {
                    Equipment = equipment,
                });
                this.userRepository.Update(appUser);
                await this.userRepository.SaveChangesAsync();
                this.equipmentsRepository.Update(equipment);
                await this.equipmentsRepository.SaveChangesAsync();
            }
            else
            {
                userEquipment.Quantity++;
                this.userEquipmentRepository.Update(userEquipment);
                await this.userEquipmentRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteEquipmentByIdAsync(int equipmentId)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == equipmentId).FirstOrDefault();
            this.equipmentsRepository.Delete(equipment);
            var equipmentsInCart = this.userEquipmentRepository.All().Where(x => x.EquipmentId == equipmentId).ToList();
            foreach (var item in equipmentsInCart)
            {
                this.userEquipmentRepository.Delete(item);
            }

            await this.userEquipmentRepository.SaveChangesAsync();
            await this.equipmentsRepository.SaveChangesAsync();
        }
    }
}
