using HPowerTunings.Services.Appointment;
using HPowerTunings.ViewModels.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(CreateAppointmetModel model)
        {
            if (ModelState.IsValid)
            {
                if (appointmentService.CreateAppointment(model))
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
    }
}