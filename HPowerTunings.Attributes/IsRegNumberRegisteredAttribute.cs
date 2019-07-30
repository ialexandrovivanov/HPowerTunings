using HPowerTunings.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HPowerTunings.Attributes
{
    public class IsRegNumberRegisteredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("");
            
            var context = (ApplicationDbContext)validationContext
                        .GetService(typeof(ApplicationDbContext));

            if (context.Cars.Any(u => u.RegNumber == value.ToString()))
            {
                return new ValidationResult("A car with this reg. number is already registered");
            }
            return ValidationResult.Success;
        }
    }
}
