using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HPowerTunings.Data;
using HPowerTunings.ViewModels.CarModels;
using HPowerTunings.ViewModels.EmployeeModels;
using HPowerTunings.ViewModels.PartModels;
using HPowerTunings.ViewModels.RepairModels;

namespace HPowerTunings.Services.Repair
{
    public class RepairService : IRepairService
    {
        ApplicationDbContext context;
        public RepairService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateRepair(CreateRepairOutputModel model)
        {
            var car = this.context.Cars.FirstOrDefault(c => c.RegNumber == model.CarRegNumber);

            this.context.Repairs.Add(new Data.Models.Repair()
            { RepairName = model.RepairName, Car = car, CarId = car.Id, Description = model.Description });

            this.context.SaveChanges();

            var repair = this.context
                             .Repairs
                             .Where(r => r.Car.RegNumber == model.CarRegNumber)
                             .OrderByDescending(r => r.CreatedOn)
                             .FirstOrDefault();

            car.Repairs.Add(repair);

            var mechanic1 = this.context.Employees.SingleOrDefault(e => e.FullName == model.Mechanic1FullName);
            var mechanic2 = this.context.Employees.SingleOrDefault(e => e.FullName == model.Mechanic2FullName);
            var emplRep = new Data.Models.EmployeeRepair();

            if (mechanic1 == mechanic2 && mechanic1 != null)
            {
                emplRep.Employee = mechanic1;
                emplRep.EmployeeId = mechanic1.Id;
                emplRep.Repair = repair;
                emplRep.RepairId = repair.Id;

                var employeeRepair = this.context.EmployeesRepairs.Add(emplRep);

                mechanic1.EmployeesRepairs.Add(emplRep);
                repair.EmployeesRepairs.Add(emplRep);

                await this.context.SaveChangesAsync();
            }

            else
            {
                if (mechanic1 != null)
                {
                    emplRep.Employee = mechanic1;
                    emplRep.EmployeeId = mechanic1.Id;
                    emplRep.Repair = repair;
                    emplRep.RepairId = repair.Id;

                    var employeeRepair = this.context.EmployeesRepairs.Add(emplRep);

                    mechanic1.EmployeesRepairs.Add(emplRep);
                    repair.EmployeesRepairs.Add(emplRep);

                    await this.context.SaveChangesAsync();
                }

                if (mechanic2 != null)
                {
                    emplRep.Employee = mechanic2;
                    emplRep.EmployeeId = mechanic2.Id;
                    emplRep.Repair = repair;
                    emplRep.RepairId = repair.Id;

                    var employeeRepair = this.context.EmployeesRepairs.Add(emplRep);

                    mechanic2.EmployeesRepairs.Add(emplRep);
                    repair.EmployeesRepairs.Add(emplRep);

                    this.context.SaveChanges();
                }
            }
              
            return repair == null ? false : true;                              
        }

        public ICollection<string> GetAllMechanicNames()
        {
            return this.context.Employees.Where(e => e.Possition == "Mechanic").Select(e => e.FullName).ToList();
        }

        public ICollection<string> GetAllRegNumbers()
        {
            return this.context.Cars.Select(c => c.RegNumber).OrderByDescending(a => a).ToList();
        }

        public ICollection<AdminRepairViewModel> GetAllRepairsPeriod(RepairStartEndDateViewModel model)
        {
            ICollection<AdminRepairViewModel> result = new List<AdminRepairViewModel>();
            var repairs = this.context
                              .Repairs
                              .Where(r => r.StartedOn >= model.StartDate && r.StartedOn <= model.EndDate)
                              .Select(r => r)
                              .ToList();

            var config = new MapperConfiguration(c => 
                         {
                         c.CreateMap<Data.Models.Repair, AdminRepairViewModel>();
                         c.CreateMap<Data.Models.Part, PartViewModel>();
                         c.CreateMap<Data.Models.Car, CarViewModel>();
                         c.CreateMap<Data.Models.Employee, EmployeeViewModel>();
                         });

            var mapper = config.CreateMapper();

            foreach (var repair in repairs)
            {
                var employeesIds = repair.EmployeesRepairs.Select(er => er.EmployeeId);
                var employees = this.context.Employees.Where(e => employeesIds.Contains(e.Id)).Select(e => e).ToList();

                var repairModel = mapper.Map<Data.Models.Repair, AdminRepairViewModel>(repair);
                repairModel.Car = mapper.Map<Data.Models.Car, CarViewModel>(repair.Car);

                if (repairModel.Car != null)
                {
                    repairModel.Car.CarBrand = repair.Car.CarBrand.Name;
                    repairModel.Car.CarModel = repair.Car.CarModel.Name;
                    repairModel.TotalIncomes = repairs.Sum(r => r.RepairPrice);
                }

                foreach (var part in repair.Parts)
                    repairModel.TotalOutgoings += part.Price;
                
                foreach (var employee in employees)
                    repairModel.Employees.Add(mapper.Map<Data.Models.Employee, EmployeeViewModel>(employee));

                result.Add(repairModel);
            }

            return result;
        }

        public Task<ProceedRepairModel> ProceedRepair(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
