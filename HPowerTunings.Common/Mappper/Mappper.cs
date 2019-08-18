using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Common.Mappper
{
    public class Mappper : IMappper
    {
        public async Task<IMapper> Mappp()
        {
            await Task.Delay(0);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<EmployeeRegisterViewModel, Data.Models.Employee>();
                c.CreateMap<Data.Models.Employee, EmployeeStartEndViewModel>();
                c.CreateMap<Data.Models.Repair, EmployeeRepairViewModel>();
            });

            return mapperConfig.CreateMapper();
        }
    }
}
