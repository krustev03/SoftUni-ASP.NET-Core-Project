namespace FitnessHub.Services.Messaging
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;

    public interface IMailService
    {
        Task SendEmailAsync(ApplicationUser applicationUser);
    }
}
