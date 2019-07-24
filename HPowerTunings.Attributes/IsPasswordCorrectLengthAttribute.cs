using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HPowerTunings.Attributes
{
    public class IsPasswordCorrectAttribute : ValidationAttribute
    {
        private const string regex = "^[a-zA-Z 0-9]{3,60}$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = Regex.IsMatch((string)value, regex);
            if (!result)
            {
                return new ValidationResult("Use letters, numbers and spaces only");
            }
            return ValidationResult.Success;
        }
    }
}
