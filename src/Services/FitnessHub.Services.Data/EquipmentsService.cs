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

    public class EquipmentsService : IEquipmentsService
    {
        private readonly IDeletableEntityRepository<Equipment> equipmentsRepository;

        public EquipmentsService(
            IDeletableEntityRepository<Equipment> equipmentsRepository)
        {
            this.equipmentsRepository = equipmentsRepository;
        }

        public async Task AddEquipmentAsync(
            AddEquipmentInputModel equipmentInputModel, ApplicationUser appUser)
        {
            var equipment = new Equipment()
            {
                Name = equipmentInputModel.Name,
                Price = decimal.Parse(equipmentInputModel.Price, CultureInfo.InvariantCulture),
                Description = equipmentInputModel.Description,
                ImageUrl = equipmentInputModel.ImageUrl,
            };

            appUser.Equipments.Add(equipment);

            await this.equipmentsRepository.AddAsync(equipment);
            await this.equipmentsRepository.SaveChangesAsync();
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
    }
}
