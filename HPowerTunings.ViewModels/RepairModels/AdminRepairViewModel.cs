using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.PartModels;
using System;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class AdminRepairViewModel
    {
        public string Id { get; set; }
        public decimal RepairPrice { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public CarViewModel Car { get; set; }
        public bool IsRepairPending { get; set; } = true;
        public ICollection<PartViewModel> Parts { get; set; } = new List<PartViewModel>();
        public ICollection<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
        public decimal TotalOutgoings { get; set; }
        public decimal TotalIncomes { get; set; }
        public double AverageAmountRepairs { get; set; }
        public string RepairName { get; set; }

    }
}
