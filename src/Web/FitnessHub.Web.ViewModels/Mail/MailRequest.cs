namespace FitnessHub.Web.ViewModels.Mail
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class MailRequest
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
