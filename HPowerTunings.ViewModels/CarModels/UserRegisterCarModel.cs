using HPowerTunings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarModels
{
    public class UserRegisterCarModel
    {
        private const string regNumberError = "Use capital letters, numbers and correct length";
        private const string ramaErrorMessage = "Use capital letters and numbers only";
        private const string distancePassedErrorMessage = "Use numbers only";
        private const string regNumberRegisteredErrorMessage = "Use numbers only";

        public string CarBrandName { get; set; }

        [Required(ErrorMessage = "Car model is required")]
        public string CarModelName { get; set; }

        [Required(ErrorMessage = "Reg. number is required")]
        [IsRegNumberRegistered(ErrorMessage = regNumberRegisteredErrorMessage)]
        [RegularExpression("^[ABCEHKLMNOPTXY]{1,2}[0-9]{4,6}[ABCEHKLMNOPTXY]{1,2}$", ErrorMessage = regNumberError)]
        public string RegNumber { get; set; }

        [Required(ErrorMessage ="VIN is required")]
        [Display(Name = "Vehicle identification number (VIN)")]
        [RegularExpression("^[a-zA-Z0-9]{2,60}$", ErrorMessage = ramaErrorMessage)]
        public string Rama { get; set; }

        [Required(ErrorMessage = "Distance is required")]
        [RegularExpression("^[0-9]{1,6}$", ErrorMessage = distancePassedErrorMessage)]
        public string DistancePassed { get; set; }
    }
}
