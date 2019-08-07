using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HPowerTunings.Common.Email
{
    public class EmailSender : ICustomEmailSender
    {
        private readonly IConfiguration configuration;
        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendEmailsAsync(string email,
                                          string subject,
                                          string imagePath,
                                          string htmlMessage)
        {
            var from = new EmailAddress("support@hpower.net", "HPowerTunings support team");
            var subj = subject;
            var to = new EmailAddress(email, "Dear Client");
            var body = htmlMessage;
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, subject, body, htmlMessage);
            var bytes = File.ReadAllBytes(@"C:\Users\Ivan\Downloads\" + imagePath);
            var file = Convert.ToBase64String(bytes);
            message.AddAttachment("picture.jpg", file);
            

            var key = this.configuration["SendGridApiKey:Key"];
            var client = new SendGridClient(key);
            var result = await client.SendEmailAsync(message);
            
            return;
        }
    }
}
