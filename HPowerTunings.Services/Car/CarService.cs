using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Car
{
    public class CarService : ICarService
    {
        private ApplicationDbContext context;
        private IHttpContextAccessor httpContext;
        private UserManager<Client> userManager;
        public CarService(ApplicationDbContext context,
                          IHttpContextAccessor httpContext,
                          UserManager<Client> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContext = httpContext;
        }

        public async Task<CarViewModel> GetCarDetailsAsync(string carId)
        {
            var car = await this.context.Cars.FindAsync(carId);
            var model = new CarViewModel()
            {
                CarBrand = car.CarBrand.Name,
                CarModel = car.CarModel.Name,
                CarId = carId
            };

            return model;
        }

        public async Task<bool> DeleteYourCar(DeleteYourCarModel model)
        {
            var car = await this.context.Cars.FindAsync(model.CarId);

            if (car != null)
            {
                car.IsDeleted = true;
                await this.context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<string>> GetAllCarBrands(string brand)
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
                              .Select(r => r);

            if (repairs.Count() == 0) return null;
          
            var getRepairsViewModel = new CarRepairsViewModel();

            getRepairsViewModel.CarModel = repairs.First().Car.CarModel.Name;
            getRepairsViewModel.CarBrand = repairs.First().Car.CarBrand.Name;
            getRepairsViewModel.CarId = carId;

            foreach (var repair in repairs)
            {
                var repairViewModel = new RepairViewModel();

                repairViewModel.Description = repair.Description;
                repairViewModel.CreatedOn = repair.CreatedOn;
                repairViewModel.FinishedOn = repair.FinishedOn;
                repairViewModel.RepairPrice = repair.RepairPrice;

                var employeesIds = repair.EmployeesRepairs.Select(er => er.EmployeeId);

                repairViewModel.Mechanics = this.context
                                                .Employees
                                                .Where(e => employeesIds.Contains(e.Id))
                                                .Select(e => new EmployeeViewModel()
                                                {
                                                    FullName = e.FullName,
                                                    Id = e.Id
                                                })
                                                .ToList();

                repairViewModel.Parts = repair.Parts
                                              .Select(p => new PartViewModel()
                                              {
                                                  Brand = p.Brand,
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Price = p.Price
                                              })
                                              .ToList();

                getRepairsViewModel.Repairs.Add(repairViewModel);
            }

            await Task.Delay(0);
            return getRepairsViewModel;
        }

        public async Task<bool> UserCreateCar(UserRegisterCarModel model)
        {
            var claimsPrincipal = this.httpContext.HttpContext.User;
            var currentUser = await this.userManager.GetUserAsync(claimsPrincipal);
            var carBrand = this.context.CarBrands.FirstOrDefault(c => c.Name == model.CarBrand);
            var carModel = carBrand.Models.FirstOrDefault(m => m.Name == model.CarModel);

            var car = new Data.Models.Car
            {
                RegistrationNumber = model.RegistrationNumber,
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
    }
}
