using HPowerTunings.ViewModels.Appointment;
using HPowerTunings.ViewModels.RepairModels;
using System.Collections.Generic;

namespace HPowerTunings.ViewModels.AdminModels
{
    public class AdminWelcomePageModel
    {
        List<PendingRepairViewModel> PendingRepairs { get; set; } 
            = new List<PendingRepairViewModel>();

        List<PendingAppointmentsViewModel> PendingAppointments { get; set; }
            = new List<PendingAppointmentsViewModel>();
    }
}
