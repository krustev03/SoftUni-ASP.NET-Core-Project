namespace FitnessHub.Services.Data.Tests
{
    using System.Linq;

    using FitnessHub.Data.Models;
    using FitnessHub.Data.Repositories;
    using FitnessHub.Web.ViewModels.Chat;
    using Xunit;

    // async Task AddMessageAsync(MessageInputModel inputModel, string authorId)

    // async Task DeleteMessageByIdAsync(int id)
    public class MessageServiceTests : BaseServiceTest
    {
        [Fact] // 1. async Task AddMessageAsync(MessageInputModel inputModel, string authorId)
        public async void AddMessageAsync_ShouldAddMessageInDatabase()
        {
            // Arrange
            var messagesRepository = new EfDeletableEntityRepository<Message>(this.Context);
            var messageService = new MessageService(messagesRepository);
            var content = "Some content for the test message.";

            // Act
            await messageService.AddMessageAsync(content, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");
            var message = await messagesRepository.GetByIdWithDeletedAsync(1);
            var expectedContent = "Some content for the test message.";
            var expectedAuthorId = "24bf72c6-e348-40d1-a7b1-d28dfa033c80";

            // Assert
            Assert.Equal(expectedContent, message.Content);
            Assert.Equal(expectedAuthorId, message.AuthorId);
        }

        [Fact] // 2. async Task DeleteMessageByIdAsync(int id)
        public async void DeleteMessageByIdAsync_ShouldDeleteMessageInDatabase()
        {
            // Arrange
            var messagesRepository = new EfDeletableEntityRepository<Message>(this.Context);
            var messageService = new MessageService(messagesRepository);

            var content = "Some content for the test message.";

            await messageService.AddMessageAsync(content, "24bf72c6-e348-40d1-a7b1-d28dfa033c80");

            // Act
            await messageService.DeleteMessageByIdAsync(1);
            var resultCount = messagesRepository.All().ToList().Count();
            var expectedCount = 0;

            // Assert
            Assert.Equal(expectedCount, resultCount);
        }
    }
}
