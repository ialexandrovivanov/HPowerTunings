using System.Diagnostics;
using HPowerTunings.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult AboutUs()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Gallery()
        {
            return this.View();
        }
    }
}
