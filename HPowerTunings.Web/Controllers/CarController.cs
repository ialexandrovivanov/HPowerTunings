using HPowerTunings.Services.Car;
using HPowerTunings.ViewModels.AdminModels;
using HPowerTunings.ViewModels.CarModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class CarController : Controller
    {
        private ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult>Index(string carBrand)
        {
            ViewData["CarBrand"] = carBrand;
            ViewData["CarModels"] = await this.carService.GetAllCarModelsAsync(carBrand);

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SuccessCreate()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(UserRegisterCarModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.carService.UserCreateCar(model);
                if (result)
                {
                    return Redirect("/Car/SuccessCreate");
                }
            }

            ViewData["CarBrand"] = model.CarBrand;
            ViewData["CarModels"] = await this.carService.GetAllCarModelsAsync(model.CarBrand);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SuccessDelete()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CarStatistic()
        {
            return this.View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var model = await this.carService.GetCarRepairsAsync(id);
            
            if (model.Repairs == null) return View(model);
           
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(string id, string pId, CarRepairsViewModel model)
        {
            bool result = await this.carService.RatePartAsync(pId, model);

            if (ModelState.IsValid && result)
            {
                return Redirect($"/PartsFromCars/SuccessRate/{id}");
            }

            model = await this.carService.GetCarRepairsAsync(model.Id);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteYourCar(string id)
        {
            if(id != null)
            {
                var carViewModel = await this.carService.GetCarDetailsAsync(id);

                ViewData["CarBrand"] = carViewModel.CarBrandName;
                ViewData["CarModel"] = carViewModel.CarModelName;
                var model = await this.carService.GetDeleteYourCar(id);
                return View(model);
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteYourCar(DeleteYourCarModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.carService.DeleteYourCar(model);
                if (result)
                {
                    return Redirect("SuccessDelete");
                }
            }

            ViewData["CarBrand"] = model.CarBrandName;
            ViewData["CarModel"] = model.CarModelName;

            return View(model);
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateCar()
        {
            var brands = await this.carService.GetAllCarBrandsAsync();
          
            return this.View(new AdminCreateCarOutputModel() { CarBrands = brands });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateCar(AdminCreateCarOutputModel model)
        {
            if (ModelState.IsValid)
            {
                return Redirect($"/Car/AdminRegisterCar?carBrand={model.CarBrand}&regNumber={model.RegNumber}");
            }

            var result = await this.carService.GetAllCarBrandsAsync();
            model.CarBrands = result;
            return  View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegisterCar(string carBrand, string regNumber)
        {
            ViewData["CarBrand"] = carBrand;
            ViewData["RegNumber"] = regNumber;
            ViewData["CarModels"] = await this.carService.GetAllCarModelsAsync(carBrand);
            var model = new AdminRegisterCarModel();
            return View(model);
        }

        [HttpPost]  
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegisterCar(AdminRegisterCarModel model)
        {
            if (ModelState.IsValid)
            {
                if (await this.carService.AdminRegisterCar(model))
                {
                    return Redirect("/Car/AdminSuccessRegisterCar");
                }
            }

            ViewData["CarBrand"] = model.CarBrand;
            ViewData["RegNumber"] = model.RegNumber;
            ViewData["CarModels"] = await this.carService.GetAllCarModelsAsync(model.CarBrand);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminSuccessRegisterCar()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CarStatisticStartEnd(CarStartEndDateViewModel model, string submit)
        {

            if (submit == "period")
            {
                if (model.StartDate > model.EndDate)
                {
                    return RedirectToAction("CarStatistic", "Car", model);
                }

                if (model.StartDate == default(DateTime) || model.EndDate == default(DateTime))
                {
                    ModelState.AddModelError(string.Empty, "Insert correct date and time");
                    return RedirectToAction("CarStatistic", "Car", model);
                }

                var result = await this.carService.GetAllCarsPeriod(model);
                if (result == null) return View();

                return View(result);
            }

            model.StartDate = DateTime.MinValue;
            model.EndDate = DateTime.MaxValue;

            var resultAll = await this.carService.GetAllCarsPeriod(model);

            if (resultAll == null) return View();

            return View(resultAll);
        }
    }
}
