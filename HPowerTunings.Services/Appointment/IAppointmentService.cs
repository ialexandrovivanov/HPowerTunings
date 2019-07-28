using HPowerTunings.ViewModels.Appointment;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Appointment
{
    public interface IAppointmentService
    {
        Task<bool> CreateAppointment(CreateAppointmetModel model);
    }
}
