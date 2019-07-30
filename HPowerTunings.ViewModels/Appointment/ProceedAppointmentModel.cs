using System;

namespace HPowerTunings.ViewModels.Appointment
{
    public class ProceedAppointmentModel
    {
        public In In { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public ProceedAppointmentModel()
        {
            this.In = new In();
        }
    }

    public class In
    {
        public string Id { get; set; }
        public string ClientUsername { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public string DesiredDate { get; set; }
        public string ProblemDescription { get; set; }
    }

}
