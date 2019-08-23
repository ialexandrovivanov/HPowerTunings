using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.Appointment;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.EntityFrameworkCore;

namespace HPowerTunings.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public CompanyService(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public DateTime DateTime { get; private set; }

        public async Task<List<PendingAppointmentsViewModel>> GetPendingAppointments()
        {
            var result = new List<PendingAppointmentsViewModel>();
            await this.context
                      .Appointments
                      .Include(a => a.Client)
                      .Where(a => (a.IsAppointmentPending == true ||
                             a.AppointmentDate.Date == DateTime.Now.Date) && a.IsAppointmentStarted == false)
                      .ForEachAsync(a =>
                       {
                           var model = mapper.Map<Data.Models.Appointment, PendingAppointmentsViewModel>(a);
                           model.CarCarBrandName = this.context
                                                       .Cars
                                                       .FirstOrDefaultAsync(c => c.RegNumber == model.RegNumber)
                                                       .GetAwaiter()
                                                       .GetResult()
                                                       .CarBrand
                                                       .Name;

                           model.CarCarModelName = this.context
                                                       .Cars
                                                       .FirstOrDefaultAsync(c => c.RegNumber == model.RegNumber)
                                                       .GetAwaiter()
                                                       .GetResult()
                                                       .CarModel
                                                       .Name;

                           result.Add(model);
                       });
                            

            return result;
        }

        public async Task<List<PendingRepairViewModel>> GetPendingRepairs()
        {
            var result =  this.context
                              .Repairs
                              .Where(r => r.IsRepairPending == true)
                              .Select(r => new PendingRepairViewModel()
                              {
                                  CarBrand = r.Car.CarBrand.Name, CarModel = r.Car.CarModel.Name,
                                  RepairId = r.Id, RegNumber = r.Car.RegNumber, ClientEmail = r.Car.Client.Email,
                                  ClientPhoneNumber = r.Car.Client.PhoneNumber, ClientUserName = r.Car.Client.UserName,
                                  RepairDescription = r.Description, RepairName = r.RepairName,
                                  StartDate = r.CreatedOn.Value.ToString("yyyy/MM/dd  -  HH:mm:ss"),
                                  Mechanics = r.EmployeesRepairs.Select(e => e.Employee.FullName).ToList(),
                                  CreatedOn = r.CreatedOn
                              })
                              .ToList();

            await Task.Delay(0);
            return result;
        }
    }
}
