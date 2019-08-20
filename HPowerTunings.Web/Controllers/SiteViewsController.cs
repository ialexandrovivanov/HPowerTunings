using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class SiteViewsController : Controller
    {
        public IActionResult ServicesBodywork()
        {
            return View();
        }

        public IActionResult ServicesDiagnostics()
        {
            return View();
        }

        public IActionResult ServicesRestoration()
        {
            return View();
        }

        public IActionResult ServicesRepair()
        {
            return View();
        }
    }
}