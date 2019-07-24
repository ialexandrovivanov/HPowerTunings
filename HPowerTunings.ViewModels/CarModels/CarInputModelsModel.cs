using System.Collections.Generic;

namespace HPowerTunings.ViewModels.CarModels
{
    public class CarInputModelsModel
    {
        public string CarBrand { get; set; }
        public IEnumerable<string> CarModels { get; set; }
    }
}
