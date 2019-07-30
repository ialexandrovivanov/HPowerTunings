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
        public IActionResult Index()
        {
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

                return View("Unable to create an appointment. Please use another way to contact to us!");
            }

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
            if (await this.appointmentService.AdminCreateAppointment(model))
                return Redirect($"/Appointment/SuccessCreateAppointment/{model.In.Id}");

            else return View("Cannot proceed your appointent");
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