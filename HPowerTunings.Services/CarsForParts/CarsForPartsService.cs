using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.CarsForParts;

namespace HPowerTunings.Services.CarsForParts
{
    public class CarsForPartsService : ICarsForPartsService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CarsForPartsService(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<CarsForPartsMainViewModelIn>> GetAllCarModelsAsync()
        {
            var cars = this.context
                           .CarsForParts
                           .Where(c => c.IsDeleted == false)
                           .Select(c => this.mapper.Map<Data.Models.CarForParts, CarsForPartsMainViewModelIn>(c))
                           .ToList();

            foreach (var car in cars)
                car.Parts = this.context
                                .PartsFromCars
                                .Where(p => p.CarForParts.Id == car.Id)
                                .Select(s => this.mapper.Map<Data.Models.PartFromCar, PartFromCarViewModel>(s))
                                .ToList();

            await Task.Delay(0);
            return cars;
        }
    }
}
