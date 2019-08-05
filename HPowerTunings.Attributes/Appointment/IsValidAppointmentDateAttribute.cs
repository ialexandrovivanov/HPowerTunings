using System;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.Attributes.Appointment
{
    public class IsValidAppointmentDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value < DateTime.Now.Date)
            {
                return new ValidationResult("Set future date for appoinment");
            }
            return ValidationResult.Success;
        }
    }
}
