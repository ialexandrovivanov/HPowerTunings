using AutoMapper;
using HPowerTunings.ViewModels.Appointment;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.CarsForParts;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.PartsFromCar;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Web.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeRegisterViewModel, Data.Models.Employee>();
            CreateMap<Data.Models.Employee, EmployeeRegisterViewModel>();
            CreateMap<Data.Models.Employee, EmployeeStartEndViewModel>();
            CreateMap<Data.Models.Repair, EmployeeRepairViewModel>();
            CreateMap<EmployeeRepairViewModel, Data.Models.Repair>();
            CreateMap<Data.Models.Car, CarViewModel>();
            CreateMap<Data.Models.Car, ClientCarDetailsViewModel>();
            CreateMap<Data.Models.Appointment, MyAppointmentsViewModel>();
            CreateMap<Data.Models.Repair, AdminRepairViewModel>();
            CreateMap<Data.Models.Part, PartViewModel>();
            CreateMap<Data.Models.Car, CarViewModel>();
            CreateMap<Data.Models.Employee, EmployeeViewModel>();
            CreateMap<Data.Models.Part, PartStatisticsViewModel>();
            CreateMap<Data.Models.Repair, ProceedRepairModelIn>();
            CreateMap<Data.Models.Appointment, PendingAppointmentsViewModel>();
            CreateMap<Data.Models.Appointment, ProceedAppointmentModelIn>();
            CreateMap<Data.Models.Car, ClientCarsViewModel>();
            CreateMap<Data.Models.Car, CarRepairsViewModel>();
            CreateMap<Data.Models.Repair, RepairViewModel>();
            CreateMap<Data.Models.Employee, EmployeeViewModel>();
            CreateMap<Data.Models.Part, PartViewModel>();
            CreateMap<UserRegisterCarModel, Data.Models.Car>();
            CreateMap<Data.Models.Car, CarStatisticViewModel>();
            CreateMap<Data.Models.Car, DeleteYourCarModel>();
            CreateMap<Data.Models.Part, RatePartViewModel>();
            CreateMap<Data.Models.Part, PartViewModel>();
            CreateMap<Data.Models.PartFromCar, PartFromCarViewModel>();
            CreateMap<Data.Models.CarForParts, CarsForPartsMainViewModelIn>();
            CreateMap<CreateCarViewModel, Data.Models.CarForParts>();
            CreateMap<SellPartViewModel, Data.Models.PartFromCar>();
        }
    }
}
