using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPowerTunings.ViewModels.CarModels
{
    public class CarRepairsViewModel
    {
        public string Id { get; set; }
        public string CarBrandName { get; set; }
        public string CarModelName { get; set; }
        public string RegNumber { get; set; }

        [Range(1, 10)]
        public int Rate { get; set; }
        public string PartId { get; set; }
        public ICollection<RepairViewModel> Repairs { get; set; } = new List<RepairViewModel>();
    }
}
