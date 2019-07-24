using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPowerTunings.Data.Models
{
    public class Repair : BaseModel
    {
        public string RepairName { get; set; }
        public decimal RepairPrice { get; set; }
        public DateTime? StartedOn { get; set; } = DateTime.Now;
        public DateTime? FinishedOn { get; set; }

        [ForeignKey("Car")]
        public string CarId { get; set; }
        public virtual Car Car { get; set; }

        public virtual ICollection<Part> Parts { get; set; } 
                     = new HashSet<Part>();
        public virtual ICollection<EmployeeRepair> EmployeesRepairs { get; set; }
                     = new HashSet<EmployeeRepair>();
        public virtual ICollection<DayRepair> DaysRepairs { get; set; }
                     = new HashSet<DayRepair>();
    }
}