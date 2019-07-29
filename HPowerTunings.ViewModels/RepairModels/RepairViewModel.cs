using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.PartModels;
using System;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class RepairViewModel
    {
        public string Id { get; set; }
        public string RepairName { get; set; }
        public decimal RepairPrice { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public ICollection<EmployeeViewModel> Mechanics { get; set; } = new List<EmployeeViewModel>();
        public ICollection<PartViewModel> Parts { get; set; } = new List<PartViewModel>();
    }
}
