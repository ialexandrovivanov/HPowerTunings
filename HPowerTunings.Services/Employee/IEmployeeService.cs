using HPowerTunings.ViewModels.EmployeeModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPowerTunings.Services.Employee
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(EmployeeRegisterViewModel model);
        Task<ICollection<EmployeeStartEndViewModel>> EmployeeStartEndStatistics
                                                     (EmployeeStartEndStatisticsViewModel model);
    }
}
