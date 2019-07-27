using HPowerTunings.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HPowerTunings.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<DayRepair> DaysRepairs { get; set; }
        public DbSet<CarForParts> CarsForParts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PartFromCar> PartsFromCars { get; set; }
        public DbSet<EmployeeRepair> EmployeesRepairs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeRepair>().HasKey(er => new { er.EmployeeId, er.RepairId });
            modelBuilder.Entity<DayRepair>().HasKey(dr => new { dr.DayId, dr.RepairId });
        }
    }
}
