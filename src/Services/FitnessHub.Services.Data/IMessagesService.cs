namespace FitnessHub.Services.Data
{
    using System.Threading.Tasks;

    using FitnessHub.Web.ViewModels.Chat;

    public interface IMessagesService
    {
        public Task AddMessageAsync(AddMessageInputModel messageInputModel, string authorId);
    }
}
