using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Core
{
    public class EmailSender1 : ICustomEmailSender
    {
        private IConfiguration configuration;
        public EmailSender1(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private static CancellationTokenSource tokenSource = new CancellationTokenSource();

        public async Task StartMails(string mail, string subject, string  message, int periodDays)
        {
            var token = tokenSource.Token;

            while (periodDays-- > 0)
            {
                Thread.Sleep(1000 * 60 * 60 * 24);

                if (!token.IsCancellationRequested)
                {
                   await Task.Run(() => SendEmailAsync(mail, subject, message));
                }
                else break;
            } 
        }

        public static void StopMails()
        {
            tokenSource.Cancel();
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email, "Dear Client"));
                message.From = new MailAddress("support@hpower.net", "HPowerTunings support team");
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                var key = this.configuration["SendGridApiKey:Key"];

                using (var client = new SmtpClient("smtp.sendgrid.net"))
                {
                    client.Port = 587;
                    client.EnableSsl = false;
                    client.Credentials = new NetworkCredential("apikey", key);
                    client.Send(message);
                }
            }

            return Task.CompletedTask;
        }

        public Task SendEmailsAsync(string content, string subject, string imagePath, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
