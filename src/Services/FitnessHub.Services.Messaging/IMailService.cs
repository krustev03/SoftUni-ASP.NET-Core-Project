namespace FitnessHub.Services.Messaging
{
    using System.Threading.Tasks;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Mail;

    public interface IMailService
    {
        Task SendEmailAsync(ApplicationUser applicationUser);
    }
}
