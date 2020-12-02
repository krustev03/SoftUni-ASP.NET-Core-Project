namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    public interface IUserService
    {
        IEnumerable<T> GetAllUsers<T>();
    }
}