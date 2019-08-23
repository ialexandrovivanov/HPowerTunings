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
            var car = await this.context.Cars.FindAsync(model.CarId);

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

        public async Task<CarRepairsViewModel> GetCarRepairs(string carId)
        {
            var repairs = this.context
                              .Repairs
                              .Where(r => r.CarId == carId && r.IsDeleted == false)
                              .Select(r => r)
                              .ToList();

            var car = await this.context.Cars.FindAsync(carId);

            var getRepairsViewModel = mapper.Map<Data.Models.Car, CarRepairsViewModel>(car);

            if (repairs != null)
            {
                foreach (var repair in repairs)
                {
                    var repairViewModel = mapper.Map<Data.Models.Repair, RepairViewModel>(repair);

                    var employeesIds = repair.EmployeesRepairs.Select(er => er.EmployeeId);

                    foreach (var rep in getRepairsViewModel.Repairs)
                    {
                        rep.Mechanics = this.context
                                            .Employees
                                            .Where(e => employeesIds.Contains(e.Id))
                                            .Select(e => mapper.Map<Data.Models.Employee, EmployeeViewModel>(e))
                                            .ToList();

                        rep.Parts = repair.Parts
                                          .Select(p => mapper.Map<Data.Models.Part, PartViewModel>(p))
                                          .ToList();
                    }
                }
            }
           
            return getRepairsViewModel;
        }

        public async Task<bool> UserCreateCar(UserRegisterCarModel model)
        {
            var claimsPrincipal = this.httpContext.HttpContext.User;
            var currentUser = await this.userManager.GetUserAsync(claimsPrincipal);

            var car = mapper.Map<UserRegisterCarModel, Data.Models.Car>(model);

            var carBrand = this.context.CarBrands.FirstOrDefault(c => c.Name == model.CarBrandName);
            var carModel = carBrand.Models.FirstOrDefault(m => m.Name == model.CarModelName);

            car.CarBrand = carBrand;
            car.CarModel = carModel;
            car.CarModelId = carModel.Id;
            car.CarBrandId = carBrand.Id;
            car.Client = currentUser;
            car.ClientId = currentUser.Id;
          
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
            var carBrand = this.context.CarBrands.FirstOrDefault(b => b.Name == model.CarBrand);
            var carModel = this.context.CarModels.FirstOrDefault(m => m.Name == model.CarModel);
            var client = this.context.Users.FirstOrDefault(c => c.Email == model.Email);


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
            return result == null ? false : true;
        }

        public async Task<ICollection<CarStatisticViewModel>> GetAllCarsPeriod(CarStartEndDateViewModel model)
        {
            ICollection<CarStatisticViewModel> result = new List<CarStatisticViewModel>();
            var cars = this.context
                              .Cars
                              .Where(r => r.CreatedOn.Value.Date >= model.StartDate.Date &&
                              r.CreatedOn.Value.Date <= model.EndDate.Date)
                              .Select(r => r);

            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Data.Models.Car, CarStatisticViewModel>();
            });

            var mapper = config.CreateMapper();

            foreach (var car in cars.OrderBy(c => c.CreatedOn))
            {
                var modelForCollection = mapper.Map<Data.Models.Car, CarStatisticViewModel>(car);
                modelForCollection.ClientEmail = car.Client?.Email;
                modelForCollection.ClientUserName = car.Client?.UserName;
                modelForCollection.RegisteredOn = car.CreatedOn.Value.ToString("MM/dd/yyyy HH:mm");
                result.Add(modelForCollection);
            }

            await Task.Delay(0);
            return result;
        }

        public async Task<DeleteYourCarModel> GetDeleteYourCar(string id)
        {
            var car = this.context.Cars.FirstOrDefault(c => c.Id == id);
            var model = new DeleteYourCarModel()
            {
                CarBrand = car.CarBrand.Name,
                CarModel = car.CarModel.Name,
                CarId = car.Id
            };

            await Task.Delay(0);
            return model;
        }

        public async Task<List<string>> GetAllBmwModels()
        {
            var models = this.context.CarModels.Where(m => m.CarBrand.Name == "BMW").Select(m => m.Name).ToList();
            await Task.Delay(0);
            return models;
        }

        public async Task<List<string>> GetAllMiniModels()
        {
            var models = this.context.CarModels.Where(m => m.CarBrand.Name == "Mini Cooper").Select(m => m.Name).ToList();
            await Task.Delay(0);
            return models;
        }

        public async Task<List<string>> GetAllRangeModels()
        {
            var models = this.context.CarModels.Where(m => m.CarBrand.Name == "Range Rover").Select(m => m.Name).ToList();
            await Task.Delay(0);
            return models;
        }
    }
}
