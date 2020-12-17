namespace FitnessHub.Services.Messaging
{
    using System.Threading.Tasks;

    using FitnessHub.Data.Models;

    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;

    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;

        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            this.mailSettings = mailSettingsOptions.Value;
        }

        public async Task SendEmailAsync(ApplicationUser applicationUser)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("fitnesshubofficial2020@gmail.com");
            email.To.Add(MailboxAddress.Parse(applicationUser.Email));
            email.Subject = "Your Order";
            var builder = new BodyBuilder();

            builder.HtmlBody = "<p>Congratulations! Your order is on the way, " + applicationUser.UserName + ". Thanks that you chose us!</p>";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(this.mailSettings.Host, this.mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.mailSettings.Mail, this.mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Dispose();
        }
    }
}
