using HPowerTunings.ViewModels.CarsForParts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.CarsForParts
{
    public interface ICarsForPartsService
    {
        Task<List<CarsForPartsMainViewModelIn>> GetAllCarModelsAsync();
        Task<bool> CreateCar(CreateCarViewModel model);
        Task<ICollection<string>> GetAllModelNamesAsync();
        Task<ICollection<string>> GetAllBrandNamesAsync();
        Task<Data.Models.CarForParts> GetCarById(string id);
        Task<bool> DeleteCar(DeleteCarViewModel model);
        Task<decimal> GetTotalIn();
        Task<decimal> GetTotalOut();
    }
}
