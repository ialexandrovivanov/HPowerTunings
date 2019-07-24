﻿using System.Collections.Generic;
using System.Linq;
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
        public ICollection<AdminRepairViewModel> GetAllRepairsPeriod(RepairStartEndDateViewModel model)
        {
            ICollection<AdminRepairViewModel> result = new List<AdminRepairViewModel>();
            var repairs = this.context
                              .Repairs
                              .Where(r => r.FinishedOn >= model.StartDate && r.FinishedOn <= model.EndDate)
                              .Select(r => r)
                              .ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Data.Models.Repair, AdminRepairViewModel>();
                cfg.CreateMap<Data.Models.Part, PartViewModel>();
                cfg.CreateMap<Data.Models.Car, CarViewModel>();
                cfg.CreateMap<Data.Models.Employee, EmployeeViewModel>();
            });
            var mapper = config.CreateMapper();

            foreach (var repair in repairs)
            {
                var employeesIds = repair.EmployeesRepairs.Select(er => er.EmployeeId);
                var employees = this.context.Employees.Where(e => employeesIds.Contains(e.Id)).Select(e => e).ToList();

                var repairModel = mapper.Map<Data.Models.Repair, AdminRepairViewModel>(repair);

                repairModel.Car = mapper.Map<Data.Models.Car, CarViewModel>(repair.Car);

                repairModel.Car.CarBrand = repair.Car.CarBrand.Name;
                repairModel.Car.CarModel = repair.Car.CarModel.Name;
                repairModel.TotalIncomes = repairs.Sum(r => r.RepairPrice);

                foreach (var part in repair.Parts)
                    repairModel.TotalOutgoings += part.Price;
                
                foreach (var employee in employees)
                    repairModel.Employees.Add(mapper.Map<Data.Models.Employee, EmployeeViewModel>(employee));

                result.Add(repairModel);
            }

            return result;
        }
    }
}
