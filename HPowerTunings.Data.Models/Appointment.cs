using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class Appointment : BaseModel
    {
        public DateTime AppointmentDate { get; set; }
        public string ProblemDescription { get; set; }
        public bool? IsAppointmentPending { get; set; } = true;

        [ForeignKey("Day")]
        public string DayId { get; set; }
        public virtual Day Day { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
