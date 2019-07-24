using HPowerTunings.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.Attributes
{
    public class IsCurrentPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<Client>)validationContext
                        .GetService(typeof(UserManager<Client>));

            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var user = userManager.GetUserAsync(httpContextAccessor.HttpContext.User).GetAwaiter().GetResult();
            var result = userManager.CheckPasswordAsync(user, (string)value).GetAwaiter().GetResult();

            if (!result)
            {
                return new ValidationResult("Provided password is not valid");
            }
            return ValidationResult.Success;
        }
    }
}
