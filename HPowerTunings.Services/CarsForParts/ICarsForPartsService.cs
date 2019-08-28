using HPowerTunings.ViewModels.CarsForParts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.CarsForParts
{
    public interface ICarsForPartsService
    {
        Task<List<CarsForPartsMainViewModelIn>> GetAllCarModelsAsync();
    }
}
