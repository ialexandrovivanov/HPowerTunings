using System;

namespace HPowerTunings.ViewModels.EmployeeModels
{
    public class EmployeeStartEndStatisticsViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployeeRegisterViewModel RegisterEmployee { get; set; } = new EmployeeRegisterViewModel();
        public EmployeeDeleteViewModel DeleteEmployee { get; set; } = new EmployeeDeleteViewModel();
    }
}
