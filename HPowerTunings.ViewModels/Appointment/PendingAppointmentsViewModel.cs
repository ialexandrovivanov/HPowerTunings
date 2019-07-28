using System;

namespace HPowerTunings.ViewModels.Appointment
{
    public class PendingAppointmentsViewModel
    {
        public string AppointmentId { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ProblemDescription { get; set; }
        public string DesiredDate { get; set; }
    }
}
