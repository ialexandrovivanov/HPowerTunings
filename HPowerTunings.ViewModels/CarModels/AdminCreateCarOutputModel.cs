using HPowerTunings.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarModels
{
    public class AdminCreateCarOutputModel
    {
        [Required(ErrorMessage = "Select car brand")]
        public string CarBrand { get; set; }

        [Required]
        [IsRegNumberRegistered]
        public string RegNumber { get; set; }

        public ICollection<string> CarBrands { get; set; } = new List<string>();
    }
}
