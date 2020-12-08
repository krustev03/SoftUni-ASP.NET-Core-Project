namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;

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

        public async Task<IEnumerable<string>> GetLast(string content)
        {
            var message = this.messagesRepository.All().Where(x => x.Content == content).FirstOrDefault();

            List<string> data = new List<string>
            {
                message.Id.ToString(),
                message.CreatedOn.ToString(),
            };

            return await Task.Run(() => data);
        }

        public async Task DeleteMessageByIdAsync(int messageId)
        {
            var message = this.messagesRepository.All().Where(x => x.Id == messageId).FirstOrDefault();
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
