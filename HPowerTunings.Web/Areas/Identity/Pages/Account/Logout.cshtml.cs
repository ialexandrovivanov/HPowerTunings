using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HPowerTunings.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HPowerTunings.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Client> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<Client> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task OnGet()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"{User.Identity.Name} logged out.");
            Redirect("/");
        }
    }
}