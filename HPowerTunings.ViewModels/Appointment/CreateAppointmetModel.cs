using System;
using System.ComponentModel.DataAnnotations;
using HPowerTunings.Attributes.Appointment;

namespace HPowerTunings.ViewModels.Appointment
{
    public class CreateAppointmetModel
    {

        private const string CorrectDateTimeMessage = "Insert correct date and time";
        private const string RequiredErrorMessage = "Description is required";

        [IsValidAppointmentDate]
        [Required(ErrorMessage = CorrectDateTimeMessage)]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Description { get; set; }
        public bool IsAppointmentPending { get; set; }
        public bool IsAppointmentApproved { get; set; }
    }
}
