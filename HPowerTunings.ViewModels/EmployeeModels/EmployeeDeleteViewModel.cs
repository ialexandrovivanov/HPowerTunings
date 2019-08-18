using HPowerTunings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.EmployeeModels
{
    public class EmployeeDeleteViewModel
    {
        public string EmployeeFullName { get; set; }

        public string Password { get; set; }
    }
}
