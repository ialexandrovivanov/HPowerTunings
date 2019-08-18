using System;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class EmployeeRepairViewModel
    {
        public string Id { get; set; }
        public string RepairName { get; set; }
        public decimal RepairPrice { get; set; }
        public string Description { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public string CarNumber { get; set; }
        public bool IsRepairPending { get; set; } = true;
        public int CountOfMechanics { get; set; }
    }
}
