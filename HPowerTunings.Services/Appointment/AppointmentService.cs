using HPowerTunings.Data;
using HPowerTunings.ViewModels.Appointment;
using Microsoft.AspNetCore.Http;
using System;
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

            day.Appointments.Add(new Data.Models.Appointment
            {
                DesiredDate = model.DesiredDate,
                Description = model.Description,
                ClientId = clientId,
                Client = client,
                DayId = day.Id,
                IsAppointmentPending = true,
                AppointmentDate = null
            });

            var result = await this.context.SaveChangesAsync();

            return result == 1 ? true : false;
        }

        public async Task<ProceedAppointmentModel> GetAppoinmentDetails(string id)
        {
            var appointment = this.context.Appointments.FirstOrDefault(a => a.Id == id);

            var result = new ProceedAppointmentModel();

            result.In.ClientEmail = appointment.Client.Email;
            result.In.Id = id;
            result.In.DesiredDate = appointment.DesiredDate.Value.ToString("yyyy/MM/dd");
            result.In.ProblemDescription = appointment.ProblemDescription;
            result.In.ClientPhone = appointment.Client.PhoneNumber;
            result.In.ClientUsername = appointment.Client.UserName;

            await Task.Delay(0);
            return result;
        }

        public async Task<bool> AdminCreateAppointment(ProceedAppointmentModel model)
        {
            var appointment = this.context.Appointments.FirstOrDefault(a => a.Id == model.In.Id);

            appointment.AppointmentDate = model.AppointmentDate;
            appointment.IsAppointmentPending = false;

            var result = await this.context.SaveChangesAsync();

            return result == 0 ? false : true;
        }
    }
}
