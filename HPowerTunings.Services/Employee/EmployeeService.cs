using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext context;

        public EmployeeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateEmployee(EmployeeRegisterViewModel model)
        {
            var employee = new Data.Models.Employee()
                           {
                               Email = model.Email,
                               Address = model.Address,
                               FullName = model.FullName,
                               PhoneNumber = model.PhoneNumber,
                               Possition = model.Possition,
                           };

            await this.context.Employees.AddAsync(employee);
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<ICollection<EmployeeStartEndViewModel>> EmployeeStartEndStatistics
                                                                  (EmployeeStartEndStatisticsViewModel model)
        {
            var result = this.context
                             .Employees
                             .Select(e => new EmployeeStartEndViewModel()
                             {
                                 Id = e.Id,
                                 Email = e.Email,
                                 FullName = e.FullName,
                                 Possition = e.Possition,
                             })
                             .ToList();

            foreach (var mod in result)
            {
                var repairsIds = this.context
                                     .EmployeesRepairs
                                     .Where(er => er.EmployeeId == mod.Id)
                                     .Select(er => er.RepairId)
                                     .ToList();

                var repairs = this.context
                                  .Repairs
                                  .Where(r => repairsIds.Contains(r.Id) &&
                                         r.StartedOn >= model.StartDate &&
                                         r.StartedOn <= model.EndDate)
                                  .Select(r => new EmployeeRepairViewModel()
                                  {
                                      CarNumber = r.Car.RegNumber,
                                      Description = r.Description,
                                      CreatedOn = r.StartedOn,
                                      FinishedOn = r.FinishedOn,
                                      IsRepairPending = r.IsRepairPanding,
                                      CountOfMechanics = r.EmployeesRepairs.Count
                                  })
                                  .ToList();

                mod.Repairs = repairs;
            }
            await Task.Delay(0);
            return result;
        }
    }
}
