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
        public ICollection<string> CarModelsBmw { get; set; } = new List<string>();
        public ICollection<string> CarModelsMini { get; set; } = new List<string>();
        public ICollection<string> CarModelsRange { get; set; } = new List<string>();

        public string CarModel { get; set; }
        public string Rama { get; set; }
        public int DistancePassed { get; set; }
        public string ClientEmail { get; set; }
    }
}
