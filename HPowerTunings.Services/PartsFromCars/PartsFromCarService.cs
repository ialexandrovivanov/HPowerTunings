using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.PartModels;
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
