using HPowerTunings.Services.Employee;
using HPowerTunings.ViewModels.EmployeeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
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

    }
}