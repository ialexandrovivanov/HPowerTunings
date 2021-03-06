using HPowerTunings.Services.Part;
using HPowerTunings.Services.Repair;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    
    public class RepairController : Controller
    {
        private readonly IRepairService repairService;
        private readonly IPartService partService;
        public RepairController(IRepairService repairService, IPartService partService)
        {
            this.partService = partService;
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
        public IActionResult RepairStartEnd(RepairStartEndDateViewModel model, string submit)
        {
            if (submit == "period")
            {
                if (model.StartDate > model.EndDate)
                    return RedirectToAction("Index", "Repair", model);

                if (!ModelState.IsValid)
                    return RedirectToAction("Index", "Repair", model);

                var result = this.repairService.GetAllRepairsPeriod(model);
                if (result == null) return View();

                return View(result);
            }

            model.StartDate = DateTime.MinValue;
            model.EndDate = DateTime.MaxValue;

            var resultAll = this.repairService.GetAllRepairsPeriod(model);
            if (resultAll == null) return View();

            return View(resultAll);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StartRepair(string id)
        {
            var result = await this.repairService.StartRepair(id);
            var regNumber = result?.RegNumber;
            var description = result?.ProblemDescription;

            if (result != null)
            {
                return Redirect($"/Repair/CreateRepair?regNumber={regNumber}&description={description}");
            }

            return Redirect("/Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRepair(string regNumber = null, string description = null)
        {
            ViewData["AllNames"] = this.repairService.GetAllMechanicNames().ToList();
            ViewData["RegNumber"] = regNumber;
            ViewData["Description"] = description;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRepair(CreateRepairOutputModel model)
        {
            if (!ModelState.IsValid) 
            {
                ViewData["AllNames"] = this.repairService.GetAllMechanicNames().ToList();
                return View(model);
            }

            var result = await this.repairService.CreateRepair(model);
            if (result)
            {
                return Redirect("/Repair/SuccessCreate");
            }
            else
            {
                ViewData["AllNames"] = this.repairService.GetAllMechanicNames().ToList();
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult SuccessCreate()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProceedRepair(string id)
        {
            var model = await this.repairService.ProceedRepair(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FinalizeRepair(ProceedRepairModel model)
        {
            if (model.Out.RepairPrice <= 0)
            {
                return RedirectToAction("ProceedRepair", "Repair", new { id = model.In.Id });
            }
            else
            {
                var result = await this.repairService.FinalizeRepair(model);
                if (result)
                {
                    return Redirect("/Repair/SuccessFinalize");
                }

                return RedirectToAction("ProceedReair","Repair", new { id = model.In.Id });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuccessFinalize()
        {
            await Task.Delay(0);
            return View();
        }
    }
}