using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}