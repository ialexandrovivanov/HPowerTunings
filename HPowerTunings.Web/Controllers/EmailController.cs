using HPowerTunings.Services.Email;
using HPowerTunings.ViewModels.EmailModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        private readonly IEmailSender emailSender;

        public EmailController(IEmailService emailService, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.emailService = emailService;
        }

        [HttpGet]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Index()
        {
            ViewData["CarModels"] = await this.emailService.GetAllModels();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> SendEmails(SendEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CarModels"] = await this.emailService.GetAllModels();
                return View("Index");
            }

            var result = await this.emailService.SendEmailsAsync(model);
            if (result)
            {
                return View();
            }
            return Redirect("Index");
        }
    }
}