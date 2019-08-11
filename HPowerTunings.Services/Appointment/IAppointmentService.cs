using HPowerTunings.ViewModels.Appointment;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Appointment
{
    public interface IAppointmentService
    {
        Task<bool> CreateAppointment(CreateAppointmetModel model);
        Task<ProceedAppointmentModel> GetAppoinmentDetails(string id);
        Task<bool> AdminCreateAppointment(ProceedAppointmentModel model);
        Task<ICollection<MyAppointmentsViewModel>> GetMyAppointments();
    }
}
