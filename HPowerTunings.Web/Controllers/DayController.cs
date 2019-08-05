using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class DayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}