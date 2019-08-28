using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarsForParts
{
    public class CarsForPartsMainViewModel
    {
        public CarsForPartsMainViewModelOut Out { get; set; } 
                = new CarsForPartsMainViewModelOut();
        public ICollection<CarsForPartsMainViewModelIn> Cars { get; set; } 
                = new List<CarsForPartsMainViewModelIn>();
    }

    public class CarsForPartsMainViewModelOut
    {
        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string CarModel { get; set; }

        public string Vin { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public decimal InitialPrice { get; set; }
    }

    public class CarsForPartsMainViewModelIn
    {
        public string Id { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string Rama { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public ICollection<PartFromCarViewModel> Parts { get; set; }
               = new List<PartFromCarViewModel>();
    }
}