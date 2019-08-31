using HPowerTunings.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.AdminModels
{
    public class AdminRegisterCarModel
    {
        private const string regNumberError = "Use capital letters, numbers and correct length";
        private const string ramaErrorMessage = "Use capital letters and numbers only";
        private const string distancePassedErrorMessage = "Use numbers only";

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Insert valid email address")]
        [IsEmailExists]
        public string Email { get; set; }
        [RegularExpression("^[ABCEHKLMNOPTXY]{1,2}[0-9]{4,6}[ABCEHKLMNOPTXY]{1,2}$", ErrorMessage = regNumberError)]
        public string RegNumber { get; set; }
        public string CarBrand { get; set; }
        [Required]
        public string CarModel { get; set; }

        [Display(Name = "Vehicle identification number (VIN)")]
        [RegularExpression("^[a-zA-Z0-9]{2,60}$", ErrorMessage = ramaErrorMessage)]
        public string Rama { get; set; }
        [Required]
        [RegularExpression("^[0-9]{1,7}$", ErrorMessage = distancePassedErrorMessage)]
        public int DistancePassed { get; set; }
    }
}
