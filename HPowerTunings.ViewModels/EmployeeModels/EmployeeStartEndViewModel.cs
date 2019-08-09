using HPowerTunings.ViewModels.RepairModels;
using System;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.EmployeeModels
{
    public class EmployeeStartEndViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Possition { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime? HiredDate { get; set; } = DateTime.Now;
        public DateTime? FiredDate { get; set; }
        public ICollection<EmployeeRepairViewModel> Repairs { get; set; } = new HashSet<EmployeeRepairViewModel>();
    }
}
