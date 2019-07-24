using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HPowerTunings.Data.Models;
using HPowerTunings.Data;
using System.Linq;
using HPowerTunings.Attributes;

namespace HPowerTunings.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Client> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<Client> userManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [IsEmailTaken]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(60)]
            [IsUsernameTaken]
            [IsUsernameCorrect]
            public string UserName { get; set; }

            [Required]
            [StringLength(60, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)

            {
                var user = new Client { UserName = Input.UserName, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                var countUsers = _userManager.Users.Count();
                var isEmployeeEmail = _context.Employees
                                              .Where(e => e.IsDeleted == false)
                                              .Select(e => e.Email)
                                              .Any(e => e == user.Email);

                if (result.Succeeded && countUsers == 1)
                {
                    IdentityResult roleResultAdmin =
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    IdentityResult roleResultUser =
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    IdentityResult roleResultEmployee =
                        await _roleManager.CreateAsync(new IdentityRole("Employee"));
                    IdentityResult roleResultManager =
                        await _roleManager.CreateAsync(new IdentityRole("Manager"));

                    var res = await _userManager.AddToRoleAsync(user, "Admin");
                }
                else if (result.Succeeded && isEmployeeEmail)
                {
                    var res = await _userManager.AddToRoleAsync(user, "Employee");
                }
                else if (result.Succeeded)
                {
                    var res = await _userManager.AddToRoleAsync(user, "User");
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page("/Account/ConfirmEmail",
                                               pageHandler: null,
                                               values: new { userId = user.Id, code = code },
                                               protocol: Request.Scheme);
                    
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email address",
                        $"Please confirm the existence of your email by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicking here</a>");

                    return RedirectToPage("./CheckEmail");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
