namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Equipments;
    using Microsoft.AspNetCore.Identity;

    public class EquipmentsService : IEquipmentsService
    {
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserEquipment> userEquipmentRepository;

        public EquipmentsService(
            IDeletableEntityRepository<Equipment> equipmentsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserEquipment> userEquipmentRepository)
        {
            this.equipmentsRepository = equipmentsRepository;
            this.userRepository = userRepository;
            this.userEquipmentRepository = userEquipmentRepository;
        }

        public async Task AddEquipmentAsync(EquipmentInputModel equipmentInputModel)
        {
            var equipment = new Equipment()
            {
                Name = equipmentInputModel.Name,
                Price = decimal.Parse(equipmentInputModel.Price, CultureInfo.InvariantCulture),
                Description = equipmentInputModel.Description,
                ImageUrl = equipmentInputModel.ImageUrl,
            };

            await this.equipmentsRepository.AddAsync(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
        }

        public async Task EditEquipment(int id, EquipmentInputModel equipmentInputModel)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            equipment.Name = equipmentInputModel.Name;
            equipment.Price = decimal.Parse(equipmentInputModel.Price, CultureInfo.InvariantCulture);
            equipment.Description = equipmentInputModel.Description;
            equipment.ImageUrl = equipmentInputModel.ImageUrl;

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

        public int GetCount()
        {
            return this.equipmentsRepository.All().Count();
        }

        public IEnumerable<T> GetAllEquipments<T>()
        {
            return this.equipmentsRepository.All().To<T>();
        }

        public T GetEquipmentDetails<T>(int id)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return equipment;
        }

        public async Task DeleteEquipmentByIdAsync(int id)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.equipmentsRepository.Delete(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
        }

        public async Task AddEquipmentToCart(int id, string userId)
        {
            var equipment = this.equipmentsRepository.All().Where(x => x.Id == id).FirstOrDefault();
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
    }
}
