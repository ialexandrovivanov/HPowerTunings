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
            var model = new CarsForPartsMainViewModel();
            model.Cars = cars;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCar(CarsForPartsMainViewModel model)
        {
            if (!ModelState.IsValid)
            {
                List<CarsForPartsMainViewModelIn> cars = await this.carsForPartsService.GetAllCarModelsAsync();
                var model1 = new CarsForPartsMainViewModel();
                model.Cars = cars;
                return View(model1);
            }

            var result = await this.carsForPartsService.CreateCar(model.Out);
            if (result)
            {
                return Redirect("SuccessCreateCar");
            }

            List<CarsForPartsMainViewModelIn> cars1 = await this.carsForPartsService.GetAllCarModelsAsync();
            var model2 = new CarsForPartsMainViewModel();
            model.Cars = cars1;
            return View(model2);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SellPart(string id)
        {
            await Task.Delay(0);
            var model = new SellPartViewModel() { CarForPartsId = id };
            return View(model);
        }
    }
}