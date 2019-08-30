using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.PartsFromCar;
using System.Threading.Tasks;

namespace HPowerTunings.Services.PartsFromCars
{
    public interface IPartsFromCarsService
    {
        Task<RatePartViewModel> GetPartDetailsAsync(string pId);
        Task<bool> RatePartAsync(RatePartViewModel model);
        Task<bool> CreatePart(SellPartViewModel model);
    }
}