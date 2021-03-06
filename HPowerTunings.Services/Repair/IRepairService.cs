using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Repair
{
    public interface IRepairService
    {
        ICollection<AdminRepairViewModel> GetAllRepairsPeriod(RepairStartEndDateViewModel model);
        ICollection<string> GetAllRegNumbers();
        Task<bool> CreateRepair(CreateRepairOutputModel model);
        ICollection<string> GetAllMechanicNames();
        Task<ProceedRepairModel> ProceedRepair(string id);
        Task<ICollection<string>> GetSuppliers();
        Task<bool> FinalizeRepair(ProceedRepairModel model);
        Task<Data.Models.Appointment> StartRepair(string id);
    }
}