namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    public interface IMuscleGroupService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}