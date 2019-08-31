using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class SiteViewsController : Controller
    {
        [HttpGet]
        public IActionResult ServicesBodywork()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ServicesDiagnostics()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ServicesRestoration()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ServicesRepair()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Team()
        {
            return View();
        }
    }
}