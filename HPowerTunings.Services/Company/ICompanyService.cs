using HPowerTunings.ViewModels.Appointment;
using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Company
{
    public interface ICompanyService
    {
        Task<List<PendingRepairViewModel>> GetPendingRepairs();
        Task<List<PendingAppointmentsViewModel>> GetPendingAppointments();
    }
}
