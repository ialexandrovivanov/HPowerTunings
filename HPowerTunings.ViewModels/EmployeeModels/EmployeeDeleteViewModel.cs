using HPowerTunings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.EmployeeModels
{
    public class EmployeeDeleteViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string EmployeeFullName { get; set; }

        [Required(ErrorMessage = "Required")]
        [IsCurrentPassword]
        public string Password { get; set; }
    }
}
