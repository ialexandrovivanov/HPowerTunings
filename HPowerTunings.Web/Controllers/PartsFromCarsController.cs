using System;
using System.Threading.Tasks;
using HPowerTunings.Services.Car;
using HPowerTunings.Services.PartsFromCars;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.PartModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class PartsFromCarsController : Controller
    {
        private readonly IPartsFromCarsService partsFromCarsService;
        public PartsFromCarsController(PartsFromCarsService partsFromCarsService)
        {
            this.partsFromCarsService = partsFromCarsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RatePart(string id, string pId)
        {
            if (pId == null || id == null)
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
            bool result = await this.partsFromCarsService.RatePartAsync(model);

            if (ModelState.IsValid && result)
            {
                return Redirect($"/Car/Details/{model.RepairId}");
            }

            return Redirect($"/PartsFromCars/RatePart?id={model.RepairId}&pId={model.Id}");
        }

        [HttpGet]
        [Authorize]
        public IActionResult SuccessRate(string id)
        {
            ViewData["Id"] = id;
            return View();
        }
    }
}