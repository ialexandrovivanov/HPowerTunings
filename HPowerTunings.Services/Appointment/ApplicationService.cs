using HPowerTunings.Data;
using HPowerTunings.ViewModels.Appointment;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

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
        public bool CreateAppointment(CreateAppointmetModel model)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
            var day = this.context.Days.FirstOrDefault(d => d.DayDateTime.Value.Date == DateTime.Now.Date);
            day.Appointments.Add(new Data.Models.Appointment
            {
                DesiredDate = model.DesiredDate,
                Description = model.ProblemDescription,
                Client = user,
            });
            var result = this.context.SaveChangesAsync().GetAwaiter().GetResult();
            return result == 1 ? true : false;
        }
    }
}
