using HPowerTunings.Services.Car;
using HPowerTunings.ViewModels.CarModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult>Index(string carBrand)
        {
            ViewData["CarBrand"] = carBrand;
            ViewData["CarModels"] = await this.carService.GetAllCarModels(carBrand);

            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> SuccessCreate()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
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
            ViewData["CarModels"] = await this.carService.GetAllCarModels(model.CarBrand);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SuccessDelete()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpGet]
        public IActionResult CarStatistic()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string carId)
        {
            var model = await this.carService.GetCarRepairs(carId);

            if (model == null) return View();
           
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteYourCar(string carId)
        {
            var carViewModel = await this.carService.GetCarDetailsAsync(carId);
            var deleteYourCarModel = new DeleteYourCarModel()
                                     {
                                          CarModel = carViewModel.CarModel,
                                          CarBrand = carViewModel.CarBrand,
                                          CarId = carViewModel.CarId
                                     };
            return View(deleteYourCarModel);
        }

        [HttpPost]
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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AdminCreateCar()
        {
            var result = await Task.Run(() => this.carService.GetAllCarBrands());
            return this.View(new AdminCreateCarOutputModel() { CarBrands = result });
        }
    }
}
