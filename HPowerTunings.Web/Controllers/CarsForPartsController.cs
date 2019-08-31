using AutoMapper;
using HPowerTunings.Services.CarsForParts;
using HPowerTunings.ViewModels.CarsForParts;
using HPowerTunings.ViewModels.PartsFromCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class CarsForPartsController : Controller
    {
        private readonly ICarsForPartsService carsForPartsService;

        public CarsForPartsController(ICarsForPartsService carsForPartsService)
        {
            this.carsForPartsService = carsForPartsService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<CarsForPartsMainViewModelIn> cars = await this.carsForPartsService.GetAllCarModelsAsync();
            var model = new CarsForPartsViewModel();
            model.Cars = cars;

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCar(string carModel = null,
                                                   string carBrand = null,
                                                   string color = null,
                                                   string vin = null,
                                                   string regNumber = null)
        {
            var model = new CreateCarViewModel();
            model.AllBrands = await this.carsForPartsService.GetAllBrandNamesAsync();
            model.AllModels = await this.carsForPartsService.GetAllModelNamesAsync();
            model.CarModel = carModel;
            model.CarBrand = carBrand;
            model.Color = color;
            model.Rama = vin;
            model.RegNumber = regNumber;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCar(CreateCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateCar", "CarsForParts",
                                        new
                                        {
                                            carModel = model.CarModel,
                                            carBrand = model.CarBrand,
                                            color = model.Color,
                                            vin = model.Rama,
                                            regNumber = model.RegNumber
                                        });
            }

            var result = await this.carsForPartsService.CreateCar(model);
            if (result)
            {
                return Redirect("SuccessCreateCar");
            }

            return RedirectToAction("CreateCar", "CarsForParts",
                                        new
                                        {
                                            carModel = model.CarModel,
                                            carBrand = model.CarBrand,
                                            color = model.Color,
                                            vin = model.Rama,
                                            regNumber = model.RegNumber
                                        });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SellPart(string id)
        {
            await Task.Delay(0);
            var model = new SellPartViewModel() { CarForPartsId = id };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult SuccessCreateCar()
        {
            return View();
        }
    }
}