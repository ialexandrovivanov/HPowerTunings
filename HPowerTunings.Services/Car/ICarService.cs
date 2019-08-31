using HPowerTunings.ViewModels.AdminModels;
using HPowerTunings.ViewModels.CarModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Car
{
    public interface ICarService
    {
        Task<ICollection<string>> GetAllCarModelsAsync(string brand);
        Task<bool> UserCreateCar(UserRegisterCarModel model);
        Task<CarRepairsViewModel> GetCarRepairsAsync(string carId);
        Task<bool> DeleteYourCar(DeleteYourCarModel model);
        Task<CarViewModel> GetCarDetailsAsync(string carId);
        Task<ICollection<string>> GetAllCarBrandsAsync();
        Task<bool> AdminRegisterCar(AdminRegisterCarModel model);
        Task<ICollection<CarStatisticViewModel>> GetAllCarsPeriod(CarStartEndDateViewModel model);
        Task<DeleteYourCarModel> GetDeleteYourCar(string id);
        Task<List<string>> GetAllBmwModels();
        Task<List<string>> GetAllMiniModels();
        Task<List<string>> GetAllRangeModels();
        Task<bool> RatePartAsync(string pId, CarRepairsViewModel model);
    }
}
