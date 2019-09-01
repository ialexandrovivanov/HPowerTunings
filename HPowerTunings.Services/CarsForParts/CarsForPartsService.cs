using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using HPowerTunings.ViewModels.CarsForParts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HPowerTunings.Services.CarsForParts
{
    public class CarsForPartsService : ICarsForPartsService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Data.Models.Client> userManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CarsForPartsService(ApplicationDbContext context, 
                                   IMapper mapper, 
                                   UserManager<Data.Models.Client> userManager,
                                   IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateCar(CreateCarViewModel model)
        {
            var car = this.mapper.Map<CreateCarViewModel, Data.Models.CarForParts>(model);
            if (car != null)
            {
                var res =  await this.context.CarsForParts.AddAsync(car);
                if (res != null)
                {
                    await this.context.SaveChangesAsync();
                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<bool> DeleteCar(DeleteCarViewModel model)
        {
            if (string.IsNullOrEmpty(model.Password)) return false;

            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var result = await userManager.CheckPasswordAsync(user, (string)model.Password);

            if (result)
            {
                var car = await this.context.CarsForParts.FindAsync(model.Id);
                car.IsDeleted = true;
                await this.context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ICollection<string>> GetAllBrandNamesAsync()
        {
            await Task.Delay(0);
            return this.context.CarBrands.Select(c => c.Name).ToList();
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

        public async Task<ICollection<string>> GetAllModelNamesAsync()
        {
            await Task.Delay(0);
            return this.context.CarModels.Select(m => m.Name).OrderBy(x => x).ToList();
        }

        public async Task<CarForParts> GetCarById(string id)
        {
            return await this.context.CarsForParts.FindAsync(id);
        }

        public async Task<decimal> GetTotalIn()
        {
            await Task.Delay(0);
            return this.context.CarsForParts.Sum(c => c.Price);
        }

        public async Task<decimal> GetTotalOut()
        {
            await Task.Delay(0);
            return this.context.CarsForParts.SelectMany(c => c.PartsFromCar).Sum(p => p.Price);
        }
    }
}
