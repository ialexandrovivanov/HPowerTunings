﻿using HPowerTunings.Services.Repair;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
            if (model.StartDate > model.EndDate)
                ModelState.AddModelError(string.Empty, "Insert correct period of time");

            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Repair", model);

            var result = this.repairService.GetAllRepairsPeriod(model);
            if (result == null)
            {
                return View();
            }

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRepair()
        {
            ViewData["AllNames"] = this.repairService.GetAllMechanicNames().ToList();

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
            var result = await this.repairService.ProceedRepair(id);
            return View();
        }
    }
}