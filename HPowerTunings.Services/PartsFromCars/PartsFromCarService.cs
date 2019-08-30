using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.PartsFromCar;
using System.Threading.Tasks;

namespace HPowerTunings.Services.PartsFromCars
{
    public class PartsFromCarsService : IPartsFromCarsService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PartsFromCarsService(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<bool> CreatePart(SellPartViewModel model)
        {
            var car = await this.context.CarsForParts.FindAsync(model.CarForPartsId);
            if (car != null)
            {
                var part = this.mapper.Map<SellPartViewModel, Data.Models.PartFromCar>(model);
                part.CarForPartsId = car.Id;
                part.CarForParts = car;
                await this.context.PartsFromCars.AddAsync(part);
                var result = await this.context.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        public async Task<RatePartViewModel> GetPartDetailsAsync(string pId)
        {
            var part = await this.context.Parts.FindAsync(pId);
            var result = mapper.Map<Data.Models.Part, RatePartViewModel>(part);
            return result;
        }

        public async Task<bool> RatePartAsync(RatePartViewModel model)
        {
            var part = await this.context.Parts.FindAsync(model.Id);
            part.ClientRating = model.Rate;
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
