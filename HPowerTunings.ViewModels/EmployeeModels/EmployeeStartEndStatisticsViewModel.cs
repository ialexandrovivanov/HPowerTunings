using System;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.EmployeeModels
{
    public class EmployeeStartEndStatisticsViewModel
    {
        private const string RequiredErrorMessage = "Insert correct date and time";

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime? EndDate { get; set; }

        public EmployeeRegisterViewModel RegisterEmployee { get; set; } = new EmployeeRegisterViewModel();
        public EmployeeDeleteViewModel DeleteEmployee { get; set; } = new EmployeeDeleteViewModel();
    }
}
