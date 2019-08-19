using HPowerTunings.Data;
using HPowerTunings.Data.Models;
using System;
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
        public async Task SeedDatabase()
        {
            var rnd = new Random();

            if (!this.context.CarBrands.Any())
            {
                await this.context.CarBrands.AddAsync(new CarBrand { Name = "BMW", IsDeleted = false, Id = "bmw" });
                await this.context.CarBrands.AddAsync(new CarBrand { Name = "Mini Cooper", IsDeleted = false, Id = "mini" });
                await context.CarBrands.AddAsync(new CarBrand { Name = "Range Rover", IsDeleted = false, Id = "range" });
                await context.SaveChangesAsync();
            }

            if (!this.context.CarModels.Any())
            {
                var modelsBMW = new List<string>() { "E28", "E30", "E32", "E34", "Z1", "E31", "E36", "E38", "Z3", "E39", "E46", "E53", "E52", "E65", "E85", "E60", "E63", "E83", "E87", "E90", "E70", "E71", "F01", "E89", "E84", "F06", "F12", "F10", "F25", "F20", "F30", "I01", "F32", "F22", "F15", "F45", "I12", "F26", "F16", "F48", "G11", "G30", "G32", "F39", "G01", "G02", "G05", "G15", "G29", "G20", "F40", "G06" };
                var carBrand = this.context.CarBrands.FirstOrDefault(cb => cb.Name == "BMW");
                await InsertRecords(carBrand, modelsBMW);

                var modelsMini = new List<string>() { "Mini Hatch/Hardtop (2001 to 2006)", "Mini Convertible/Cabrio (2005 to 2008)", "Mini Hatch/Hardtop (2007 to 2014)", "Mini Clubman (2008 to 2014)", "Mini Convertible (2009 to 2015)", "Mini Countryman (2011 to 2016)", "Mini Coupé (2012 to 2015)", "Mini Roadster (2012 to 2015)", "Mini Paceman (2013 to 2016)", "Mini Hatch/Hardtop (2014 to present)" };
                carBrand = this.context.CarBrands.FirstOrDefault(cb => cb.Name == "Mini Cooper");
                await InsertRecords(carBrand, modelsMini);

                var modelsRange = new List<string>() { "First generation (1970–1996)", "Second generation (1994–2002)", "Third generation (2002–2012)", "Fourth generation (2012–present)", "Range Rover Sport", "Range Rover Evoque", "Range Rover Velar" };
                carBrand = this.context.CarBrands.FirstOrDefault(cb => cb.Name == "Range Rover");
                await InsertRecords(carBrand, modelsRange);

                this.context.SaveChanges();
            }

            if (!this.context.Suppliers.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    var supplier = new Supplier()
                    {
                        SupplierName = $"company N{i}",
                        Email = $"company N{i}@company N{i}.com",
                        PhoneNumber = "0" + (i + 10000000),
                        Description = $"This description refers to supplier with number {i}",
                        Url = $"www.randomUrl{i}.com",
                    };
                    this.context.Suppliers.Add(supplier);
                }

                this.context.SaveChanges();
            }

            //if (this.context.Clients.Count() == 2)
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        var client = new Client()
            //        {
            //            UserName = $"client registered with N{i}",
            //            Email = $"client{i}@client{i}.com",
            //            EmailConfirmed = true,
            //            PhoneNumber = "0" + (10000000 + i),
            //            PasswordHash = "AQAAAAEAACcQAAAAEIalW+n0eka8nBxBXwEVZTZlgUsEBbQkOi4QYRKn//C01V88ItJV1JN289VwnfQMjw==",
            //        };

            //        this.context.Clients.Add(client);
            //    }

            //    this.context.SaveChanges();
            //}

            //if (!this.context.Cars.Any())
            //{
            //    var carBrands = new List<string>() { "bmw", "mini", "range" };
            //    var clientsIds = this.context.Clients.Select(m => m.Id).ToList();
            //    foreach (var brand in carBrands)
            //    {
            //        var models = this.context.CarModels.Where(m => m.CarBrand.Name == brand).Select(m => m.Name).ToList();
            //        for (int i = 0; i < 333; i++)
            //        {
            //            var indexCarModel = rnd.Next(0, models.Count - 1);
            //            //var indexClient = rnd.Next(0, clientsIds.Count - 1);
            //            //var client = this.context.Clients.FirstOrDefault(c => c.Id == clientsIds[indexClient]);
            //            var carBrand = this.context.CarBrands.FirstOrDefault(c => c.Id == brand);
            //            var carModel = this.context.CarModels.FirstOrDefault(c => c.Name == models[indexCarModel]);
            //            var carForDb = new Car()
            //            {
            //                //CarBrandId = brand,
            //                //CarBrand = carBrand,
            //                //CarModelId = models[indexCarModel],
            //                //CarModel = carModel,
            //                //ClientId = clientsIds[indexClient],
            //                //Client = client,
            //                Description = $"this is car description for car with N" + i,
            //                RegistrationNumber = $"CA" + rnd.Next(1000, 9999) + "AC",
            //                Rama = $"JWB" + rnd.Next(9999, 99999999),
            //                TraveledDistance = rnd.Next(9999, 999999),
            //            };

            //            this.context.Cars.Add(carForDb);
            //        }

            //        this.context.SaveChanges();
            //    }


            //    var cars = this.context.Cars.Select(c => c).ToList();

            //    foreach (var client in this.context.Clients)
            //    {
            //        var car = cars[rnd.Next(0, cars.Count - 1)];
            //        var carsClient = new List<Car>(); carsClient.Add(car); cars.Remove(car);
            //    }

            //    this.context.SaveChanges();
            //}

          

            //if (!this.context.Parts.Any())
            //{
            //    var supplliersIds = this.context.Suppliers.Select(s => s.Id).ToList();

            //    for (int i = 0; i < 1000; i++)
            //    {
            //        var part = new Part()
            //        {
            //            Brand = $"brand name N{i}",
            //            Description = $"part description N{i}",
            //            Name = $"part name N{i}",
            //            Price = rnd.Next(10, 350),
            //            SupplierId = supplliersIds[rnd.Next(0, supplliersIds.Count - 1)],
            //        };

            //        this.context.Parts.Add(part);
            //    }

            //    this.context.SaveChanges();
            //}


            //if (!this.context.Repairs.Any())
            //{
            //    var carIds = this.context.Cars.Select(c => c.Id).ToList();
            //    var parts = this.context.Parts.Select(p => p).ToList();
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        var partsFordb = new List<Part>();
            //        var numberParts = rnd.Next(1, 6);

            //        while (numberParts-- > 0)
            //            partsFordb.Add(parts[rnd.Next(0, parts.Count - 1)]);

            //        var startDate = DateTime.Parse(rnd.Next(1, 12) + "/" + rnd.Next(1, 12) + "/" + 2019);
            //        var endDate = startDate.AddDays(rnd.Next(0, 2));

            //        var repair = new Repair()
            //        {
            //            CarId = carIds[rnd.Next(0, carIds.Count - 1)],
            //            RepairName = $"repair name N{i}",
            //            RepairPrice = rnd.Next(60, 1600),
            //            Parts = partsFordb,
            //            StartedOn = startDate,
            //            FinishedOn = endDate,
            //            Description = $"this is very description of repair N{i}",
            //        };
            //        this.context.Repairs.Add(repair);
            //    }
            //    this.context.SaveChanges();
            //}

            if (!this.context.Employees.Any())
            {
                var firstNames = new List<string>() { "Ivan", "Stoyan", "Pesho", "Niki", "Petkan", "Dragan", "Mitko", "Ceci" };
                var lasNames = new List<string>() { "Ivanov", "Stoyanov", "Peshov", "Nikitov", "Petkov", "Draganov", "Mitkov", "Cvetanov" };
                for (int i = 0; i < 10; i++)
                {
                    var employees = new Employee()
                    {
                        Address = $"Sofia, street{i}, N{i}",
                        Description = "description",
                        FullName = firstNames[rnd.Next(0, firstNames.Count - 1)] + " " + lasNames[rnd.Next(0, lasNames.Count - 1)],
                        HiredDate = DateTime.Parse($"{rnd.Next(1, 12)}-{rnd.Next(1, 30)}-2019"),
                        PhoneNumber = "0" + (10000000 + i),
                        Possition = "mechanic",
                        Salary = rnd.Next(1000, 2000),
                    };
                    this.context.Employees.Add(employees);
                }

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
