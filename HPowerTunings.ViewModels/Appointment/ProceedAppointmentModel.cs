using System;

namespace HPowerTunings.ViewModels.Appointment
{
    public class ProceedAppointmentModel
    {
        public ProceedAppointmentModelIn In { get; set; } = new ProceedAppointmentModelIn();
        public DateTime AppointmentDate { get; set; }
    }

    public class ProceedAppointmentModelIn
    {
        public string Id { get; set; }
        public string ClientUserName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string AppointmentDate { get; set; }
        public string ProblemDescription { get; set; }
        public string CarModelName { get; set; }
        public string CarBrandName { get; set; }
        public string RegNumber { get; set; }
    }
}
