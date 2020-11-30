namespace FitnessHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Web.ViewModels.Chat;

    public class MessageService : IMessageService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessageService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task AddMessageAsync(MessageInputModel inputModel, string authorId)
        {
            var message = new Message()
            {
                Content = inputModel.Content,
                AuthorId = authorId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteMessageByIdAsync(int id)
        {
            var message = this.messagesRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
