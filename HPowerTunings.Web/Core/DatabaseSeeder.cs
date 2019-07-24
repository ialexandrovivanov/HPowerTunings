using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPowerTunings.Web.Core
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private ApplicationDbContext context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task SeedCarModels()
        {
            if (!this.context.CarBrands.Any())
            {
                await this.context.CarBrands.AddAsync(new CarBrand { Name = "BMW", IsDeleted = false });
                await this.context.CarBrands.AddAsync(new CarBrand { Name = "Mini Cooper", IsDeleted = false });
                await context.CarBrands.AddAsync(new CarBrand { Name = "Range Rover", IsDeleted = false });
                await context.SaveChangesAsync();
            }

            if (!this.context.CarModels.Any())
            {
                var modelsBMW = new List<string>() { "E28", "E30", "E32", "E34", "Z1", "E31", "E36", "E38", "Z3", "E39", "E46", "E53", "E52", "E65", "E85", "E60", "E63", "E83", "E87", "E90", "E70", "E71", "F01", "E89", "E84", "F06", "F12", "F10", "F25", "F20", "F30", "I01", "F32", "F22", "F15", "F45", "I12", "F26", "F16", "F48", "G11", "G30", "G32", "F39", "G01", "G02", "G05", "G15", "G29", "G20", "F40", "G06" };
                var carBrand =  this.context.CarBrands.FirstOrDefault(cb => cb.Name == "BMW");
                await InsertRecords(carBrand, modelsBMW);

                var modelsMini = new List<string>() { "Mini Hatch/Hardtop (2001 to 2006)", "Mini Convertible/Cabrio (2005 to 2008)", "Mini Hatch/Hardtop (2007 to 2014)", "Mini Clubman (2008 to 2014)", "Mini Convertible (2009 to 2015)", "Mini Countryman (2011 to 2016)", "Mini Coupé (2012 to 2015)", "Mini Roadster (2012 to 2015)", "Mini Paceman (2013 to 2016)", "Mini Hatch/Hardtop (2014 to present)" };
                carBrand =  this.context.CarBrands.FirstOrDefault(cb => cb.Name == "Mini Cooper");
                await InsertRecords(carBrand, modelsMini);

                var modelsRange = new List<string>() { "First generation (1970–1996)", "Second generation (1994–2002)", "Third generation (2002–2012)", "Fourth generation (2012–present)", "Range Rover Sport", "Range Rover Evoque", "Range Rover Velar" };
                carBrand =  this.context.CarBrands.FirstOrDefault(cb => cb.Name == "Range Rover");
                await InsertRecords(carBrand, modelsRange);

                this.context.SaveChanges();
            }
        }

        public async Task InsertRecords(CarBrand carBrand, List<string> models)
        {
            foreach (var model in models)
            {
                await this.context.CarModels.AddAsync(new CarModel()
                {
                    CarBrand = carBrand,
                    CarBrandId = carBrand.Id,
                    Name = model
                });
            }
        }
    }
}
