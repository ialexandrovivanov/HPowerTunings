using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;

namespace HPowerTunings.Services.Repair
{
    public interface IRepairService
    {
        ICollection<AdminRepairViewModel> GetAllRepairsPeriod(RepairStartEndDateViewModel model);
    }
}