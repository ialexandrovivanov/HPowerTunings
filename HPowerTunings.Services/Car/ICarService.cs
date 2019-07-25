using HPowerTunings.ViewModels.CarModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Car
{
    public interface ICarService
    {
        Task<IEnumerable<string>> GetAllCarModels(string brand);
        Task<bool> UserCreateCar(UserRegisterCarModel model);
        Task<CarRepairsViewModel> GetCarRepairs(string carId);
        Task<bool> DeleteYourCar(DeleteYourCarModel model);
        Task<CarViewModel> GetCarDetailsAsync(string carId);
        ICollection<string> GetAllCarBrands();
    }
}
