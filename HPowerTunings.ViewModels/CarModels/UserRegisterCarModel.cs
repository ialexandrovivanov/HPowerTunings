using HPowerTunings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarModels
{
    public class UserRegisterCarModel
    {
        private const string regNumberError = "Use capital letters, numbers and correct length";
        private const string ramaErrorMessage = "Use capital letters and numbers only";
        private const string distancePassedErrorMessage = "Use numbers only";

        public string CarBrand { get; set; }

        [Required(ErrorMessage = "Car model is required")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Reg. number is required")]
        [IsRegNumberRegistered(ErrorMessage ="A car with the same reg. number is already registered")]
        [RegularExpression("^[ABCEHKLMNOPTXY]{1,2}[0-9]{4,6}[ABCEHKLMNOPTXY]{1,2}$", ErrorMessage = regNumberError)]
        public string RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Vehicle identification number (VIN)")]
        [RegularExpression("^[a-zA-Z0-9]{2,60}$", ErrorMessage = ramaErrorMessage)]
        public string Rama { get; set; }

        [Required]
        [RegularExpression("^[0-9]{1,6}$", ErrorMessage = distancePassedErrorMessage)]
        public string DistancePassed { get; set; }
    }
}
