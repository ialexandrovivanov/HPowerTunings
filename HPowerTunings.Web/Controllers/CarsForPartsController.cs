using HPowerTunings.Services.CarsForParts;
using HPowerTunings.ViewModels.CarsForParts;
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<CarsForPartsMainViewModelIn> cars = await this.carsForPartsService.GetAllCarModelsAsync();
            var model = new CarsForPartsMainViewModel();
            model.Cars = cars;
            return View(model);
        }
    }
}