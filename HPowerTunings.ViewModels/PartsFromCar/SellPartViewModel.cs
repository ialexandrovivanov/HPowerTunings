using System;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.PartsFromCar
{
    public class SellPartViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Range(typeof(decimal), "0.0", "999999")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? SaledOn { get; set; } = DateTime.Now;
        public string CarForPartsId { get; set; }
    }
}
