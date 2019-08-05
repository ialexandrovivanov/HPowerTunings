using HPowerTunings.ViewModels.RepairModels;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Supplier
{
    public interface ISupplierService
    {
        Task<bool> CreateSupplier(ProceedRepairModel model);
    }
}
