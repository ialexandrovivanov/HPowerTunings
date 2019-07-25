using System.Collections.Generic;

namespace HPowerTunings.ViewModels.CarModels
{
    public class AdminCreateCarOutputModel
    {
        public string CarBrand { get; set; }
        public string ClientEmail { get; set; }
        public ICollection<string> CarBrands { get; set; } = new List<string>();
    }
}
