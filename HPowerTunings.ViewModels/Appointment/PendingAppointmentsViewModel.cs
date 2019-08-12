﻿namespace HPowerTunings.ViewModels.Appointment
{
    public class PendingAppointmentsViewModel
    {
        public string AppointmentId { get; set; }
        public string ClientEmail { get; set; }
        public string UserName { get; set; }
        public string RegNumber { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }        
        public string ClientPhoneNumber { get; set; }
        public string ProblemDescription { get; set; }
        public string AppoinmentDate { get; set; }
        public bool? IsAppointmentPending { get; set; }
        public bool? IsAppointmentStarted { get; set; }
    }
}
