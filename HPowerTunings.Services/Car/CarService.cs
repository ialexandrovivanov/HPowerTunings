using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.AdminModels;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Car
{
    public class CarService : ICarService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<Data.Models.Client> userManager;
        public CarService(ApplicationDbContext context,
                          IHttpContextAccessor httpContext,
                          UserManager<Data.Models.Client> userManager,
                          IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
            this.httpContext = httpContext;
        }

        public async Task<CarViewModel> GetCarDetailsAsync(string carId)
        {
            var car = await this.context
                                .Cars
                                .Include(c => c.CarBrand)
                                .Include(c => c.CarModel)
                                .FirstOrDefaultAsync(c => c.Id == carId);

            var model = mapper.Map<Data.Models.Car, CarViewModel>(car);
            ;
            return model;
        }

        public async Task<bool> DeleteYourCar(DeleteYourCarModel model)
        {
            var car = await this.context.Cars.FindAsync(model.Id);

            if (car != null)
            {
                car.IsDeleted = true;
                car.RegNumber = "Deleted";
                await this.context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ICollection<string>> GetAllCarModels(string brand)
        {
            var result = this.context
                             .CarModels
                             .Where(cm => cm.CarBrand.Name == brand)
                             .Select(cm => cm.Name)
                             .OrderBy(n => n)
                             .ToList();

            await Task.Delay(0);
            return result;
        }

        public async Task<CarRepairsViewModel> GetCarRepairsAsync(string carId)
        {
            var car = await this.context.Cars.FindAsync(carId);

            var mainModel = mapper.Map<Data.Models.Car, CarRepairsViewModel>(car);

            if (car.Repairs != null)
            {
                mainModel.Repairs = car.Repairs.Select(r => mapper.Map<Data.Models.Repair, RepairViewModel>(r)).ToList();
            }

            foreach (var repair in mainModel.Repairs)
            {
                var r = await this.context.Repairs.FindAsync(repair.Id);
                var employeeIds = r.EmployeesRepairs.Select(er => er.EmployeeId).ToList();

                repair.Mechanics = this.context
                                       .Employees
                                       .Where(e => employeeIds.Contains(e.Id))
                                       .Select(e => e.FullName)
                                       .ToList();

                foreach (var p in repair.Parts)
                {
                    var rating = this.context
                                     .Parts
                                     .Where(x => x.Name == p.Name)
                                     .Select(x => x.ClientRating)
                                     .Sum();

                    var allParts = this.context
                                       .Parts
                                       .Where(part => part.Name == p.Name)
                                       .Count();

                    p.Rating = (double)rating / (double)allParts;
                }
            }

            return mainModel;
        }

        public async Task<bool> UserCreateCar(UserRegisterCarModel model)
        {
            var claimsPrincipal = this.httpContext.HttpContext.User;
            var currentUser = await this.userManager.GetUserAsync(claimsPrincipal);
            var carBrand = this.context.CarBrands.FirstOrDefault(c => c.Name == model.CarBrand);
            var carModel = carBrand.Models.FirstOrDefault(m => m.Name == model.CarModel);

            var car = new Data.Models.Car
            {
                RegNumber = model.RegNumber,
                Rama = model.Rama,
                TraveledDistance = int.Parse(model.DistancePassed),
                ClientId = currentUser.Id,
                CarModelId = carModel.Id,
                CarBrandId = carBrand.Id
            };

            await this.context.Cars.AddAsync(car);
            await this.context.SaveChangesAsync();

            return true;
        }

        public ICollection<string> GetAllCarBrands()
        {
            return this.context.CarBrands.Select(c => c.Name).ToList();
        }

        public async Task<bool> AdminRegisterCar(AdminRegisterCarModel model)
        {
            var carBrand = await this.context.CarBrands.FirstOrDefaultAsync(b => b.Name == model.CarBrand);
            var carModel = await this.context.CarModels.FirstOrDefaultAsync(m => m.Name == model.CarModel);
            var client = await this.context.Users.FirstOrDefaultAsync(c => c.Email == model.Email);


            var carForDb = new Data.Models.Car()
            {
                RegNumber = model.RegNumber,
                CarBrand = carBrand,
                CarModel = carModel,
                CarBrandId = carBrand.Id,
                CarModelId = carModel.Id,
                Client = client,
                ClientId = client.Id,
                Rama = model.Rama,
                IsDeleted = false,
                TraveledDistance = model.DistancePassed,
            };

            var result = await this.context.Cars.AddAsync(carForDb);
            await this.context.SaveChangesAsync();
            return result != null;
        }

        public async Task<ICollection<CarStatisticViewModel>> GetAllCarsPeriod(CarStartEndDateViewModel model)
        {
            ICollection<CarStatisticViewModel> result = new List<CarStatisticViewModel>();
            var cars = await this.context
                           .Cars
                           .Where(r => r.CreatedOn.Value.Date >= model.StartDate.Date &&
                                  r.CreatedOn.Value.Date <= model.EndDate.Date)
                           .Select(r => r).ToListAsync();

            foreach (var car in cars.OrderBy(c => c.CreatedOn))
            {
                var modelForCollection = mapper.Map<Data.Models.Car, CarStatisticViewModel>(car);
                modelForCollection.ClientEmail = car.Client?.Email;
                modelForCollection.ClientUserName = car.Client?.UserName;
                modelForCollection.RegisteredOn = car.CreatedOn.Value.ToString("MM/dd/yyyy HH:mm");
                result.Add(modelForCollection);
            }

            return result;
        }

        public async Task<DeleteYourCarModel> GetDeleteYourCar(string id)
        {
            var car = await this.context.Cars.FindAsync(id);
            var model = mapper.Map<Data.Models.Car, DeleteYourCarModel>(car);

            return model;
        }

        public async Task<List<string>> GetAllBmwModels()
        {
            var models = await this.context
                                   .CarModels
                                   .Where(m => m.CarBrand.Name == "BMW")
                                   .Select(m => m.Name)
                                   .ToListAsync();
            return models;
        }

        public async Task<List<string>> GetAllMiniModels()
        {
            var models = await this.context
                                   .CarModels
                                   .Where(m => m.CarBrand.Name == "Mini Cooper")
                                   .Select(m => m.Name)
                                   .ToListAsync();

            return models;
        }

        public async Task<List<string>> GetAllRangeModels()
        {
            var models = await this.context
                                   .CarModels
                                   .Where(m => m.CarBrand.Name == "Range Rover")
                                   .Select(m => m.Name)
                                   .ToListAsync();

            return models;
        }

        public async Task<bool> RatePartAsync(string pId, CarRepairsViewModel model)
        {
            var part = await this.context.Parts.FindAsync(pId);
            if (part == null) return false;
            part.ClientRating = model.Rate;
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
