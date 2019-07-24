using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HPowerTunings.Attributes
{
    public class IsUsernameCorrectAttribute : ValidationAttribute
    {
        private const string regex = "^[a-zA-Z 0-9]{1,30}$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = Regex.IsMatch((string)value, regex);
            if (!result)
            {
                return new ValidationResult("Use letters spaces and numbers only");
            }
            return ValidationResult.Success;
        }
    }
}
