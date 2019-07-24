using HPowerTunings.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HPowerTunings.Attributes
{

    public class IsUsernameTakenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContextAccessor = (IHttpContextAccessor)validationContext
                        .GetService(typeof(IHttpContextAccessor));

            var userManager = (UserManager<Client>)validationContext
                        .GetService(typeof(UserManager<Client>));

            var username = httpContextAccessor.HttpContext.User.Identity.Name;

            if (username == (string)value)
            {
                return ValidationResult.Success;
            }
            if (userManager.Users.Any(u => u.UserName == value.ToString()))
            {
                return new ValidationResult("Username is already taken");
            }
            return ValidationResult.Success;
        }
    }
}
