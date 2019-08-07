﻿using HPowerTunings.ViewModels.AdminModels;
using HPowerTunings.ViewModels.CarModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Car
{
    public interface ICarService
    {
        Task<ICollection<string>> GetAllCarModels(string brand);
        Task<bool> UserCreateCar(UserRegisterCarModel model);
        Task<CarRepairsViewModel> GetCarRepairs(string carId);
        Task<bool> DeleteYourCar(DeleteYourCarModel model);
        Task<CarViewModel> GetCarDetailsAsync(string carId);
        ICollection<string> GetAllCarBrands();
        Task<bool> AdminRegisterCar(AdminRegisterCarModel model);
        Task<ICollection<CarStatisticViewModel>> GetAllCarsPeriod(CarStartEndDateViewModel model);
        Task<DeleteYourCarModel> GetDeleteYourCar(string id);
        Task<List<string>> GetAllBmwModels();
        Task<List<string>> GetAllMiniModels();
        Task<List<string>> GetAllRangeModels();
    }
}
