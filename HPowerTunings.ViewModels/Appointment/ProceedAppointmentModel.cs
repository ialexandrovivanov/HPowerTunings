using System;

namespace HPowerTunings.ViewModels.Appointment
{
    public class ProceedAppointmentModel
    {
        public In In { get; set; } = new In();
        public DateTime AppointmentDate { get; set; }
    }

    public class In
    {
        public string Id { get; set; }
        public string ClientUsername { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public string AppoinemtnDate { get; set; }
        public string ProblemDescription { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public string RegNumber { get; set; }
    }
}
