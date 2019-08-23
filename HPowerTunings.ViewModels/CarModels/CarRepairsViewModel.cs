using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.CarModels
{
    public class CarRepairsViewModel
    {
        public string Id { get; set; }
        public string CarBrandName { get; set; }
        public string CarModelName { get; set; }
        public string RegNumber { get; set; }
        public ICollection<RepairViewModel> Repairs { get; set; } = new List<RepairViewModel>();
    }
}
