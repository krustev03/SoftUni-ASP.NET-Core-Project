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

        public async Task AddMessageAsync(string messageContent, string authorId)
        {
            var message = new Message()
            {
                Content = messageContent,
                AuthorId = authorId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteMessageByIdAsync(int messageId)
        {
            var message = this.messagesRepository.All().Where(x => x.Id == messageId).FirstOrDefault();
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
