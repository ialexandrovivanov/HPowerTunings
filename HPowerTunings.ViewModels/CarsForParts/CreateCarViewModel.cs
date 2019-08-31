using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarsForParts
{
    public class CreateCarViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string CarBrand { get; set; }

        [Required(ErrorMessage = "Required")]
        public string CarModel { get; set; }
        public string Color { get; set; }
        public string RegNumber { get; set; }
        public string Rama { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(typeof(decimal), "0.1", "9999999")]
        public decimal Price { get; set; }

        public ICollection<string> AllModels { get; set; } = new List<string>();
        public ICollection<string> AllBrands { get; set; } = new List<string>();
    }
}
