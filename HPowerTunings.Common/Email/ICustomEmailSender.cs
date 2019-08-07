using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace HPowerTunings.Common.Email
{
    public interface ICustomEmailSender : IEmailSender
    {
        Task SendEmailsAsync(string content, string subject, string imagePath, string htmlMessage);
    }
}
