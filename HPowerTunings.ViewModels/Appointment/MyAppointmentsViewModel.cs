using System;

namespace HPowerTunings.ViewModels.Appointment
{
    public class MyAppointmentsViewModel
    {
        public DateTime AppointmentDate { get; set; }
        public string ProblemDescription { get; set; }
        public bool? IsAppointmentPending { get; set; }
    }
}
