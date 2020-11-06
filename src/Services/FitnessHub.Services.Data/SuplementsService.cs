namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.Suplements;

    public class SuplementsService : ISuplementsService
    {
        private readonly IDeletableEntityRepository<Suplement> suplementsRepository;

        public SuplementsService(IDeletableEntityRepository<Suplement> suplementsRepository)
        {
            this.suplementsRepository = suplementsRepository;
        }

        public async Task AddSuplementAsync(AddSuplementInputModel suplementInputModel)
        {
            var suplement = new Suplement()
            {
                Name = suplementInputModel.Name,
                Price = decimal.Parse(suplementInputModel.Price, CultureInfo.InvariantCulture),
                Description = suplementInputModel.Description,
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
    }
}
