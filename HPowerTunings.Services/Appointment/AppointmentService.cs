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
    }
}
