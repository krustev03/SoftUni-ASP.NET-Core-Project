namespace FitnessHub.Web.Hubs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;
    using FitnessHub.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            this.messageService = messageService;
            this.userManager = userManager;
        }

        public async Task Send(string message)
        {
            var appUser = await this.userManager.GetUserAsync(this.Context.User);

            await this.messageService.AddMessageAsync(message, appUser.Id);
            var lastMessage = await this.messageService.GetLast(message);
            await this.Clients.All.SendAsync("NewMessage", message, lastMessage.First(), lastMessage.Last(), appUser.UserName);
        }

        public async Task Delete(string messageId)
        {
            var id = Convert.ToInt32(messageId);

            await this.messageService.DeleteMessageByIdAsync(id);

            await this.Clients.All.SendAsync("DeletedMessage", messageId);
        }
    }
}
