using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.Appointment;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext context;

        public CompanyService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PendingAppointmentsViewModel>> GetPendingAppointments()
        {
            await Task.Delay(0);
            var result = this.context
                             .Appointments
                             .Where(a => a.IsAppointmentPending == true)
                             .Select(a => new PendingAppointmentsViewModel()
                             { AppointmentId = a.Id, ClientEmail = a.Client.Email, ClientPhoneNumber = a.Client.PhoneNumber,
                               DesiredDate = a.DesiredDate.Value.ToString("yyyy/MM/dd"), ProblemDescription = a.ProblemDescription })
                             .ToList();

            return result;
        }

        public async Task<List<PendingRepairViewModel>> GetPendingRepairs()
        {
            await Task.Delay(0);
            var result =  this.context
                              .Repairs
                              .Where(r => r.IsReairPanding == true)
                              .Select(r => new PendingRepairViewModel()
                              { CarBrand = r.Car.CarBrand.Name, CarModel = r.Car.CarModel.Name,
                                RepairId = r.Id, RegNumber = r.Car.RegistrationNumber, ClientEmail = r.Car.Client.Email,
                                ClientPhoneNumber = r.Car.Client.PhoneNumber, ClientUserName = r.Car.Client.UserName,
                                RepairDescription = r.Description, RepairName = r.RepairName,
                                StartDate = r.CreatedOn.Value.ToString("yyyy/MM/dd  -  HH:mm:ss") })
                              .ToList();

            return result;
        }
    }
}
