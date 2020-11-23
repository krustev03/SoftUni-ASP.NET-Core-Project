namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    public interface IMuscleGroupsService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}