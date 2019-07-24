using System;
using System.Collections.Generic;

namespace HPowerTunings.Data.Models
{
    public class Employee : BaseModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Possition { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HiredDate { get; set; }
        public DateTime? FiredDate { get; set; }

        public virtual ICollection<EmployeeRepair> EmployeesRepairs { get; set; }
                       = new HashSet<EmployeeRepair>();

    }
}