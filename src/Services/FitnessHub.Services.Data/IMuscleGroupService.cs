namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    public interface IMuscleGroupService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}