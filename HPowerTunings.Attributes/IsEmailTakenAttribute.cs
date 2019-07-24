using HPowerTunings.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HPowerTunings.Attributes
{
    public class IsEmailTakenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<Client>)validationContext
                        .GetService(typeof(UserManager<Client>));

            if (userManager.Users.Any(u => u.Email == value.ToString()))
            {
                return new ValidationResult("A client with this email is already registered");
            }
            return ValidationResult.Success;
        }
    }
}
