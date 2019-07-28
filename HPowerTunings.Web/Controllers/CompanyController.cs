using HPowerTunings.Services.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }


        public async Task<IActionResult> Index()
        {
            ViewData["PendingRepairs"] = await this.companyService.GetPendingRepairs();
            ViewData["PendingAppoinments"] = await this.companyService.GetPendingAppointments();
            return View();
        }
    }
}