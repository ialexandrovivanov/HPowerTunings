using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.RepairModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HPowerTunings.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Data.Models.Client> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;


        public EmployeeService(ApplicationDbContext context,
                               UserManager<Data.Models.Client> userManager,
                               IHttpContextAccessor httpContextAccessor,
                               IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<bool> CreateEmployee(EmployeeRegisterViewModel model)
        {
            var employee = mapper.Map<EmployeeRegisterViewModel, Data.Models.Employee>(model);

            await this.context.Employees.AddAsync(employee);
            var result = await context.SaveChangesAsync();

            if (result > 0) return true;

            return false;
        }

        public async Task<bool> DeleteEmployee(EmployeeDeleteViewModel deleteEmployee)
        {
            var employee = this.context.Employees.FirstOrDefault(e => e.FullName == deleteEmployee.EmployeeFullName);

            var result = this.context.Employees.Remove(employee);
            await this.context.SaveChangesAsync();

            if (result != null) return true;
        
            return false;
        }

        public async Task<ICollection<EmployeeStartEndViewModel>> EmployeeStartEndStatistics
                                                                  (EmployeeStartEndStatisticsViewModel model)
        {
            var result = this.context
                             .Employees
                             .Select(e => mapper.Map<Data.Models.Employee, EmployeeStartEndViewModel>(e))
                             .ToList();

            foreach (var mod in result)
            {
                var repairIds = this.context
                                     .EmployeesRepairs
                                     .Where(er => er.EmployeeId == mod.Id)
                                     .Select(er => er.RepairId)
                                     .ToList();
                
                var repairs = this.context
                                  .Repairs
                                  .Include(r => r.Car)
                                  .Where(r => repairIds.Contains(r.Id) &&
                                         r.StartedOn >= model.StartDate &&
                                         r.StartedOn <= model.EndDate)
                                  .Select(r => mapper.Map<Data.Models.Repair, EmployeeRepairViewModel>(r))
                                  .ToList();

                mod.Repairs = repairs;
            }
            await Task.Delay(0);
            return result;
        }

        public async Task<bool> IsEmployeeExists(EmployeeDeleteViewModel deleteEmployee)
        {
            await Task.Delay(0);
            var result = this.context.Employees.FirstOrDefault(e => e.FullName == deleteEmployee.EmployeeFullName);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsPasswordValid(string password)
        {
            if (password == null) return false;

            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var result = await userManager.CheckPasswordAsync(user, (string)password);

            if (result) return true;

            return false;
        }
    }
}
