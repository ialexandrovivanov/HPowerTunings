using HPowerTunings.Services.Repair;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    
    public class RepairController : Controller
    {
        IRepairService repairService;
        public RepairController(IRepairService repairService)
        {
            this.repairService = repairService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RepairStartEnd(RepairStartEndDateViewModel model)
        {
            var result = this.repairService.GetAllRepairsPeriod(model);

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {

            return View();
        }
    }
}