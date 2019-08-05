using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.RepairModels;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Part
{
    public interface IPartService
    {
        Task<bool> AddPart(ProceedRepairModel model);
    }
}
