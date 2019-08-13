using HPowerTunings.Data;
using HPowerTunings.ViewModels.Appointment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AppointmentService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }
        public async Task<bool> CreateAppointment(CreateAppointmetModel model)
        {
            var clientId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var client = this.context.Users.FirstOrDefault(u => u.Id == clientId);
            var day = this.context.Days.FirstOrDefault(d => d.DayDateTime.Value.Date == DateTime.Now.Date);
            var car = this.context.Cars.FirstOrDefault(c => c.RegNumber == model.RegNumber);

            if (!client.Cars.Contains(car)) return false;
            
            var appointment = new Data.Models.Appointment
            {
                Description = model.Description,
                ClientId = clientId,
                Client = client,
                DayId = day.Id,
                IsAppointmentPending = true,
                AppointmentDate = model.AppointmentDate,
                ProblemDescription = model.Description,
                RegNumber = model.RegNumber
            };

            await this.context.Appointments.AddAsync(appointment);
            day.Appointments.Add(appointment);
            client.Appointments.Add(appointment);
            var result = await this.context.SaveChangesAsync();

            return result > 0 ? true : false;
        }

        public async Task<ProceedAppointmentModel> GetAppoinmentDetails(string id)
        {
            var appointment = this.context.Appointments.FirstOrDefault(a => a.Id == id);

            var result = new ProceedAppointmentModel();
            result.AppointmentDate = appointment.AppointmentDate;
            result.In.ClientEmail = appointment.Client.Email;
            result.In.Id = id;
            result.In.AppoinemtnDate = appointment.AppointmentDate.Date.ToString("yyyy/MM/dd");
            result.In.ProblemDescription = appointment.ProblemDescription;
            result.In.ClientPhone = appointment.Client.PhoneNumber;
            result.In.ClientUsername = appointment.Client.UserName;
            result.In.ProblemDescription = appointment.ProblemDescription;
            result.In.CarBrand = this.context.Cars.FirstOrDefault(c => c.RegNumber == appointment.RegNumber).CarBrand.Name;
            result.In.CarModel = this.context.Cars.FirstOrDefault(c => c.RegNumber == appointment.RegNumber).CarModel.Name;
            result.In.RegNumber = appointment.RegNumber;

            await Task.Delay(0);
            return result;
        }

        public async Task<bool> AdminCreateAppointment(ProceedAppointmentModel model)
        {
            var appointment = await this.context.Appointments.FindAsync(model.In.Id);
            if (appointment == null)
            {
                return false;
            }
            appointment.AppointmentDate = model.AppointmentDate;
            appointment.IsAppointmentPending = false;
            appointment.IsAppointmentStarted = false;
            var result = await this.context.SaveChangesAsync();

            return result > 0 ? true : false;
        }

        public async Task<ICollection<MyAppointmentsViewModel>> GetMyAppointments()
        {
            var clientId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var client = this.context.Users.FirstOrDefault(u => u.Id == clientId);

            var appointments = this.context
                                   .Appointments
                                   .Where(a => a.AppointmentDate.Date >= DateTime.Now.Date 
                                          && a.Client == client && a.IsAppointmentPending == false)
                                   .Select(a => new MyAppointmentsViewModel()
                                   {
                                       AppointmentDate = a.AppointmentDate,
                                       ProblemDescription = a.ProblemDescription,
                                   })
                                   .ToList();

            await Task.Delay(0);
            return appointments;
        }
    }
}
