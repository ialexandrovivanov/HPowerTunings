using HPowerTunings.ViewModels.EmailModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Email
{
    public interface IEmailService
    {
        Task<List<string>> GetAllModels();
        Task<bool> SendEmailsAsync(SendEmailViewModel model);
    }
}
