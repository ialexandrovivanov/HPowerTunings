using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.CarModels
{
    public class CarRepairsViewModel
    {
        public string CarId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public ICollection<RepairViewModel> Repairs { get; set; } = new List<RepairViewModel>();
    }
}
