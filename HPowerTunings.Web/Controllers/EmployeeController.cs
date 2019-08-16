using HPowerTunings.Services.Employee;
using HPowerTunings.ViewModels.EmployeeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (await this.employeeService.CreateEmployee(model.RegisterEmployee))
            {
                return Redirect("SuccessRegisterEmployee");
            }

            ModelState.AddModelError("", "Unable to register employee");
            return View();
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

            if (await this.employeeService.IsPasswordValid(model.DeleteEmployee.Password))
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
        public async Task<IActionResult> EmployeeStatistics(EmployeeStartEndStatisticsViewModel model)
        {
            if (true)
            {

            }

           
            ModelState.AddModelError("", "Unable to register employee");
            return View();
        }

    }
}