using System;

namespace HPowerTunings.ViewModels.Appointment
{
    public class PendingAppointmentsViewModel
    {
        public string Id { get; set; }
        public string ClientEmail { get; set; }
        public string ClientUserName { get; set; }
        public string RegNumber { get; set; }
        public string CarCarBrandName { get; set; }
        public string CarCarModelName { get; set; }        
        public string ClientPhoneNumber { get; set; }
        public string ProblemDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsAppointmentPending { get; set; }
        public bool? IsAppointmentStarted { get; set; }
    }
}
