namespace FitnessHub.Services.Messaging
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Mail;

    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
