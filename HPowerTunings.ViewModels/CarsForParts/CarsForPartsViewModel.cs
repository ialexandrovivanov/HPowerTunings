using System;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.CarsForParts
{
    public class CarsForPartsViewModel
    {
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public ICollection<CarsForPartsMainViewModelIn> Cars { get; set; } 
                = new List<CarsForPartsMainViewModelIn>();

    }

    public class CarsForPartsMainViewModelIn
    {
        public string Id { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string Rama { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedOn { get; set; }
        public ICollection<PartFromCarViewModel> Parts { get; set; }
               = new List<PartFromCarViewModel>();
    }
}