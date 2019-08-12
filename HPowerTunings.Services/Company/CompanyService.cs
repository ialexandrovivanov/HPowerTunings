using System;
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
        public DateTime DateTie { get; private set; }

        public async Task<List<PendingAppointmentsViewModel>> GetPendingAppointments()
        {
            await Task.Delay(0);
            var result = this.context
                             .Appointments
                             .Where(a => (a.IsAppointmentPending == true ||
                                    a.AppointmentDate.Date == DateTime.Now.Date) && a.IsAppointmentStarted == false)
                             .Select(a => new PendingAppointmentsViewModel()
                              {
                                  AppointmentId = a.Id,
                                  ClientEmail = a.Client.Email,
                                  ClientPhoneNumber = a.Client.PhoneNumber,
                                  ProblemDescription = a.ProblemDescription,
                                  AppoinmentDate = a.AppointmentDate.ToString("yyyy/MM/dd"),
                                  IsAppointmentPending = a.IsAppointmentPending,
                                  IsAppointmentStarted = a.IsAppointmentStarted,
                                  UserName = a.Client.UserName,
                                  RegNumber = a.RegNumber,
                                  CarBrand = this.context.Cars.FirstOrDefault(c => c.RegNumber == a.RegNumber).CarBrand.Name,
                                  CarModel = this.context.Cars.FirstOrDefault(c => c.RegNumber == a.RegNumber).CarModel.Name
                             })
                             .ToList();

            return result;
        }

        public async Task<List<PendingRepairViewModel>> GetPendingRepairs()
        {
            var result =  this.context
                              .Repairs
                              .Where(r => r.IsRepairPanding == true)
                              .Select(r => new PendingRepairViewModel()
                              {
                                  CarBrand = r.Car.CarBrand.Name, CarModel = r.Car.CarModel.Name,
                                  RepairId = r.Id, RegNumber = r.Car.RegNumber, ClientEmail = r.Car.Client.Email,
                                  ClientPhoneNumber = r.Car.Client.PhoneNumber, ClientUserName = r.Car.Client.UserName,
                                  RepairDescription = r.Description, RepairName = r.RepairName,
                                  StartDate = r.CreatedOn.Value.ToString("yyyy/MM/dd  -  HH:mm:ss"),
                                  Mechanics = r.EmployeesRepairs.Select(e => e.Employee.FullName).ToList()
                              })
                              .ToList();

            await Task.Delay(0);
            return result;
        }
    }
}
