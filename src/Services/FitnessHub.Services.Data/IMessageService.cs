namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Chat;

    public interface IMessageService
    {
        public Task AddMessageAsync(MessageInputModel inputModel, string authorId);

        public Task DeleteMessageByIdAsync(int id);
    }
}
