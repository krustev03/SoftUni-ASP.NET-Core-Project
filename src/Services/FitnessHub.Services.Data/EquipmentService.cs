namespace FitnessHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Equipments;

    using static FitnessHub.Common.GlobalConstants;

    public class EquipmentService : IEquipmentService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserEquipment> userEquipmentRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly Cloudinary cloudUtility;

        public EquipmentService(
            IDeletableEntityRepository<Equipment> equipmentsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserEquipment> userEquipmentRepository,
            IRepository<Image> imagesRepository,
            Cloudinary cloudUtility)
        {
            this.equipmentsRepository = equipmentsRepository;
            this.userRepository = userRepository;
            this.userEquipmentRepository = userEquipmentRepository;
            this.imagesRepository = imagesRepository;
            this.cloudUtility = cloudUtility;
        }

        public async Task AddEquipmentAsync(CreateEquipmentInputModel model, string userId, string imagePath)
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

            // For the unit tests
            if (this.cloudUtility != null)
            {
                byte[] destinationData;
                using (var ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    destinationData = ms.ToArray();
                }

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await model.Image.CopyToAsync(fileStream);
                using (var ms = new MemoryStream(destinationData))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams
                    {
                        Folder = CloudFolder,
                        File = new FileDescription(physicalPath, ms),
                        Transformation = new Transformation().Crop("limit").Width(ImgWidth).Height(ImgHeight),
                    };

                    var img = await this.cloudUtility.UploadAsync(uploadParams);
                    equipment.Image.PublicId = img.PublicId;
                }
            }

            await this.equipmentsRepository.AddAsync(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
        }

        public async Task EditEquipment(EditEquipmentInputModel model, int equipmentId, string userId)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == equipmentId).FirstOrDefault();

            equipment.Name = model.Name;
            equipment.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
            equipment.Description = model.Description;

            this.equipmentsRepository.Update(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        {
            var equipments = this.equipmentsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return equipments;
        }

        public IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)
        {
            return this.equipmentsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public T GetEquipmentById<T>(int equipmentId)
        {
            var equipment = this.equipmentsRepository.AllAsNoTracking()
                .Where(x => x.Id == equipmentId)
                .To<T>().FirstOrDefault();
            return equipment;
        }

        public int GetCount()
        {
            return this.equipmentsRepository.All().Count();
        }

        public int GetFilteredCount(string searchString)
        {
            return this.equipmentsRepository.All()
                .Where(x => x.Name.ToLower().Contains(searchString.ToLower()))
                .Count();
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

            // For the unit tests
            if (this.cloudUtility != null)
            {
                var image = this.imagesRepository.All().Where(x => x.EquipmentId == equipmentId).FirstOrDefault();
                var imagePublicId = image.PublicId;
                string[] publicIds = { imagePublicId };
                var delParams = new DelResParams
                {
                    PublicIds = publicIds.ToList(),
                    Invalidate = true,
                };

                await this.cloudUtility.DeleteResourcesAsync(delParams);
            }

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
