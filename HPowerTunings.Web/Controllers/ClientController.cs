using Microsoft.AspNetCore.Mvc;

namespace HPowerTunings.Web.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}