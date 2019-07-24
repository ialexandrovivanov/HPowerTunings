using HPowerTunings.ViewModels.Appointment;

namespace HPowerTunings.Services.Appointment
{
    public interface IAppointmentService
    {
        bool CreateAppointment(CreateAppointmetModel model);
    }
}
