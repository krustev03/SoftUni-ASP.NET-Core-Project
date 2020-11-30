namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;

    public class MuscleGroupService : IMuscleGroupService
    {
        private readonly IRepository<MuscleGroup> muscleGroupsRepository;

        public MuscleGroupService(IRepository<MuscleGroup> muscleGroupsRepository)
        {
            this.muscleGroupsRepository = muscleGroupsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.muscleGroupsRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
