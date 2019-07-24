using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.SupplierModels;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.RepairModels
{
    public class RepairDetailsModel
    {
        ICollection<PartViewModel> Parts { get; set; }
        ICollection<SupplierViewModel> Suppliers { get; set; }
    }
}
