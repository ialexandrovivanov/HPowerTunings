using System;
using System.Threading.Tasks;
using HPowerTunings.Services.Car;
using HPowerTunings.Services.PartsFromCars;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.PartsFromCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class PartsFromCarController : Controller
    {
        private readonly IPartsFromCarsService partsFromCarsService;
        public PartsFromCarController(PartsFromCarsService partsFromCarsService)
        {
            this.partsFromCarsService = partsFromCarsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RatePart(string id, string pId)
        {
            if (id == null || pId == null)
            {
                return Redirect($"/Car/Details/{id}");
            }
            var model = await this.partsFromCarsService.GetPartDetailsAsync(pId);
            model.RepairId = id;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RatePart(RatePartViewModel model)
        {


            if (ModelState.IsValid)
            {
                bool result = await this.partsFromCarsService.RatePartAsync(model);
                if (result)
                {
                    return Redirect($"/Car/Details/{model.RepairId}");
                }

                else
                {
                    return Redirect($"/PartsFromCar/RatePart?id={model.RepairId}&pId={model.Id}");
                }

            }

            return Redirect($"/PartsFromCar/RatePart?id={model.RepairId}&pId={model.Id}");
        }

        [HttpGet]
        [Authorize]
        public IActionResult SuccessRate(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePart(SellPartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/CarsForParts/SellPart?id={model.CarForPartsId}");
            }
            var result = await this.partsFromCarsService.CreatePart(model);
            if (result)
            {
                return Redirect("SuccessCreatePart");
            }
            
            return Redirect($"/CarsForParts/SellPart?id={model.CarForPartsId}");
        }

        [HttpGet]
        [Authorize]
        public IActionResult SuccessCreatePart()
        {
            return View();
        }
    }
}