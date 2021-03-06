using HPowerTunings.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HPowerTunings.Attributes
{
    public class IsRegNumberExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext
                        .GetService(typeof(ApplicationDbContext));

            if (!context.Cars.Any(u => u.RegNumber == value.ToString()))
            {
                return new ValidationResult("A car with this number is not registered");
            }
            return ValidationResult.Success;
        }
    }
}
