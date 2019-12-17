using HPowerTunings.Services.Employee;
using HPowerTunings.ViewModels.EmployeeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string error = null)
        {
            ViewData["Errors"] = new List<string>() { error };
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(EmployeeStartEndStatisticsViewModel model)
        {
            var error = "";
            if (!ModelState.IsValid)
            {
                error = "Please fill all fields";
                return Redirect($"/Employee/Index?error={error}");
            }

            if (await this.employeeService.CreateEmployee(model.RegisterEmployee))
            {
                return Redirect("/Employee/SuccessRegisterEmployee");
            }

            return Redirect("/Employee/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuccessRegisterEmployee()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(EmployeeStartEndStatisticsViewModel model)
        {
            var error = "";
            if (!await this.employeeService.IsEmployeeExists(model.DeleteEmployee))
            {
                error = "Employee with this name doesn't exists";
                return Redirect($"/Employee/Index?error={error}");
            }

            if (!await this.employeeService.IsPasswordValid(model.DeleteEmployee.Password))
            {
                error = "Invalid password provided";
                return Redirect($"/Employee/Index?error={error}");
            }

            var result = await this.employeeService.DeleteEmployee(model.DeleteEmployee);
            if (result)
            {
                return Redirect("/Employee/SuccessDeleteEmployee");
            }

            error = "Unable to delete employee";
            return Redirect($"/Employee/Index?error={error}");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuccessDeleteEmployee()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EmployeeStatistics(EmployeeStartEndStatisticsViewModel model, string submit)
        {
            if (submit == "period")
            {
                if (model.StartDate > model.EndDate)
                {
                    return RedirectToAction("Index", "Employee", model);
                }
                if (model.StartDate == null || model.EndDate == null || (model.StartDate > model.EndDate))
                {
                    ModelState.AddModelError(string.Empty, "Insert correct date and time");
                    return Redirect("/Employee/Index");
                }

                var result = await this.employeeService.EmployeeStartEndStatistics(model);
                return View(result);
            }

            model.StartDate = DateTime.MinValue;
            model.EndDate = DateTime.MaxValue;

            var resultAll = await this.employeeService.EmployeeStartEndStatistics(model);
            if (resultAll == null) return View();
            return View(resultAll);
        }

    }
}