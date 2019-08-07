using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Core
{
    public interface ICustomEmailSender1 : IEmailSender
    {
        Task SendEmailsAsync(string content, string subject, string imagePath, string htmlMessage);
    }
}
