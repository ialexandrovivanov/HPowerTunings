using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.ClientModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace HPowerTunings.Services.Client
{
    public class CustomClientService : ICustomClientService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Data.Models.Client> userManager;
        private readonly IEmailSender emailSender;
        private string callbackUrl;

        public CustomClientService(ApplicationDbContext contex, 
                                   UserManager<Data.Models.Client> userManager,
                                   IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.context = context;
            this.emailSender = emailSender;
        }

        public async Task<bool> CreateClientAsync(AdminRegisterClientOutputModel model)
        {
            var user = new Data.Models.Client { UserName = model.UserName, Email = model.Email };
            var result = await this.userManager.CreateAsync(user, "hpowertunings");

            if (result.Succeeded)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await emailSender.SendEmailAsync(model.Email, "Confirm your email address",
                    $"Please confirm the existence of your email by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicking here</a>");

                return true;
            }

            await Task.Delay(0);
            return false;
        }
    }
}
