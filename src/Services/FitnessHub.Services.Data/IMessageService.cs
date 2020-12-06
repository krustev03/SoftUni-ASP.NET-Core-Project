namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Chat;

    public interface IMessageService
    {
        Task AddMessageAsync(string messageContent, string authorId);

        Task DeleteMessageByIdAsync(int messageId);
    }
}
