using System;
using System.ComponentModel.DataAnnotations;
using HPowerTunings.Attributes;
using HPowerTunings.Attributes.Appointment;

namespace HPowerTunings.ViewModels.Appointment
{
    public class CreateAppointmetModel
    {

        private const string CorrectDateTimeMessage = "Insert correct date and time";
        private const string RequiredErrorMessage = "Description is required";
        private const string DescriptionErrorMessage = "Use letters numbers .,:-_!?\"\' only";

        [IsValidAppointmentDate]
        [Required(ErrorMessage = CorrectDateTimeMessage)]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [RegularExpression("^[a-zA-zа-яА-Я0-9 ,.;:_()?!\"\'-]+$", ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; }

        [IsRegNumberExists]
        public string RegNumber { get; set; }
        public bool IsAppointmentPending { get; set; }
        public bool IsAppointmentApproved { get; set; }
    }
}
