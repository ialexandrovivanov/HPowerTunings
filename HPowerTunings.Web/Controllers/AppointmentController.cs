using HPowerTunings.Services.Appointment;
using HPowerTunings.ViewModels.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["MyAppointments"] = await this.appointmentService.GetMyAppointments(); 
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(CreateAppointmetModel model)
        {
            if (ModelState.IsValid)
            {
                if (await appointmentService.CreateAppointment(model))
                {
                    return Redirect("/Appointment/SuccessCreate");
                }
                ModelState.AddModelError("", "You don't have car with this number");
                ViewData["MyAppointments"] = await this.appointmentService.GetMyAppointments();
                return View();
            }

            ViewData["MyAppointments"] = await this.appointmentService.GetMyAppointments();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult SuccessCreate()
        {
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ProceedAppointment(string id)
        {
            var result = await this.appointmentService.GetAppoinmentDetails(id);
            return this.View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ProceedAppointment(ProceedAppointmentModel model)
        {
            if (model.AppointmentDate == null)
                return Redirect($"/Appointment/ProceedAppointment/{model.In.Id}");

            if (await this.appointmentService.AdminCreateAppointment(model))
                return Redirect($"/Appointment/SuccessCreateAppointment/{model.In.Id}");

            else return Redirect("/company/index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SuccessCreateAppointment(string id)
        {
            var result = await this.appointmentService.GetAppoinmentDetails(id);
            return this.View(result);
        }
    }
}