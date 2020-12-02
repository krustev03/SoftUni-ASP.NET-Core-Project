namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> usersRepository;

        public UserService(UserManager<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAllUsers<T>()
        {
            return this.usersRepository.Users.To<T>();
        }
    }
}
