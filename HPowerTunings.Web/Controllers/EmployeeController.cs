using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}