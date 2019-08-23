﻿// <auto-generated />
using System;
using HPowerTunings.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HPowerTunings.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190823052438_minorChange")]
    partial class minorChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HPowerTunings.Data.Models.Appointment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AppointmentDate");

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("DayId");

                    b.Property<string>("Description");

                    b.Property<bool?>("IsAppointmentPending");

                    b.Property<bool?>("IsAppointmentStarted");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ProblemDescription");

                    b.Property<string>("RegNumber");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DayId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Car", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarBrandId");

                    b.Property<string>("CarModelId");

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Rama");

                    b.Property<string>("RegNumber");

                    b.Property<int>("TraveledDistance");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.HasIndex("CarModelId");

                    b.HasIndex("ClientId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.CarBrand", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CarBrands");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.CarForParts", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarBrand");

                    b.Property<string>("CarModel");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Price");

                    b.Property<string>("Rama");

                    b.Property<string>("RegistrationNumber");

                    b.HasKey("Id");

                    b.ToTable("CarsForParts");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.CarModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarBrandId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Client", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Day", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DayDateTime");

                    b.Property<decimal>("DaylyExpenses");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.DayRepair", b =>
                {
                    b.Property<string>("DayId");

                    b.Property<string>("RepairId");

                    b.HasKey("DayId", "RepairId");

                    b.HasIndex("RepairId");

                    b.ToTable("DaysRepairs");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("FiredDate");

                    b.Property<string>("FullName");

                    b.Property<DateTime?>("HiredDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Possition");

                    b.Property<decimal>("Salary");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.EmployeeRepair", b =>
                {
                    b.Property<string>("EmployeeId");

                    b.Property<string>("RepairId");

                    b.HasKey("EmployeeId", "RepairId");

                    b.HasIndex("RepairId");

                    b.ToTable("EmployeesRepairs");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Part", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<int>("ClientRating");

                    b.Property<int>("CountRaters");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("RepairId");

                    b.Property<string>("SupplierId");

                    b.HasKey("Id");

                    b.HasIndex("RepairId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.PartFromCar", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarForPartsId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PartName");

                    b.Property<decimal>("Price");

                    b.Property<DateTime?>("SaledOn");

                    b.HasKey("Id");

                    b.HasIndex("CarForPartsId");

                    b.ToTable("PartsFromCars");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Repair", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FinishedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsRepairPending");

                    b.Property<string>("RepairName");

                    b.Property<decimal>("RepairPrice");

                    b.Property<DateTime?>("StartedOn");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Supplier", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int>("DeliveryRate");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SupplierName");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Appointment", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Client", "Client")
                        .WithMany("Appointments")
                        .HasForeignKey("ClientId");

                    b.HasOne("HPowerTunings.Data.Models.Day", "Day")
                        .WithMany("Appointments")
                        .HasForeignKey("DayId");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Car", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.CarBrand", "CarBrand")
                        .WithMany("Cars")
                        .HasForeignKey("CarBrandId");

                    b.HasOne("HPowerTunings.Data.Models.CarModel", "CarModel")
                        .WithMany("Car")
                        .HasForeignKey("CarModelId");

                    b.HasOne("HPowerTunings.Data.Models.Client", "Client")
                        .WithMany("Cars")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.CarModel", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.CarBrand", "CarBrand")
                        .WithMany("Models")
                        .HasForeignKey("CarBrandId");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.DayRepair", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Day", "Day")
                        .WithMany("DaysRepairs")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPowerTunings.Data.Models.Repair", "Repair")
                        .WithMany("DaysRepairs")
                        .HasForeignKey("RepairId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.EmployeeRepair", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Employee", "Employee")
                        .WithMany("EmployeesRepairs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPowerTunings.Data.Models.Repair", "Repair")
                        .WithMany("EmployeesRepairs")
                        .HasForeignKey("RepairId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Part", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Repair", "Repair")
                        .WithMany("Parts")
                        .HasForeignKey("RepairId");

                    b.HasOne("HPowerTunings.Data.Models.Supplier", "Supplier")
                        .WithMany("Parts")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.PartFromCar", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.CarForParts", "CarForParts")
                        .WithMany("PartsFromCar")
                        .HasForeignKey("CarForPartsId");
                });

            modelBuilder.Entity("HPowerTunings.Data.Models.Repair", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Car", "Car")
                        .WithMany("Repairs")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Client")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Client")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPowerTunings.Data.Models.Client")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HPowerTunings.Data.Models.Client")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
