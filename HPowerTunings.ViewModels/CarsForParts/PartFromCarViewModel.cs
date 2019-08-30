using System;

namespace HPowerTunings.ViewModels.CarsForParts
{

    public class PartFromCarViewModel
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public DateTime? SaledOn { get; set; }
    }
}
