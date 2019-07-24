using System;
using System.Collections.Generic;

namespace HPowerTunings.Data.Models
{
    public class Day : BaseModel
    {
        public DateTime? DayDateTime { get; set; } = DateTime.Now;
        public decimal DaylyExpenses { get; set; }

        public virtual ICollection<DayRepair> DaysRepairs { get; set; } = new HashSet<DayRepair>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
