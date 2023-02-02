using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_PLUS.Infrastructure.EntityTypeConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=HR_DB; Trusted_Connection=True").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyConfig());
            builder.ApplyConfiguration(new PermissionConfig());
            builder.ApplyConfiguration(new PermissionTypeConfig());
            builder.ApplyConfiguration(new AdvancePaymentConfig());
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new ExpenseConfig());

            var appRole = new AppRole
            {
                Id = 1,
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            };
            var appRole2 = new AppRole
            {
                Id = 2,
                Name = "Manager",
                NormalizedName = "MANAGER"
            };
            var appRole3 = new AppRole
            {
                Id = 3,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var company = new Company
            {
                CompanyId = 1,
                CompanyName = "BilgeAdam",
                Adress = "Caferağa Mah. Mühürdar Cad. No:76 Kadıköy / İSTANBUL​",
                PhoneNumber = "444 33 30",
                TaxIdentificationNumber = "6589424168",
                CompanyEmail = "info@bilgeadam.com",
                CreateDate = DateTime.Now,
                Status = Status.Active
            };

            var appUser = new AppUser
            {
                Id = 1,
                CompanyId = 1,
                Email = "burak.bestas@hotmail.com",
                NormalizedEmail = "burak.bestas@hotmail.com",
                EmailConfirmed = true,
                UserName = "burak.bestas@hotmail.com",
                NormalizedUserName = "BURAK.BESTAS@HOTMAIL.COM",
                Address = "İSTANBUL",
                Name = "Burak",
                Surname = "BEŞTAŞ",
                Gender = Gender.Male,
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                Status = Status.Active,
                SecurityStamp = "sfsdfsdf",
                FirstLogin = true,
                WorkingSituation = true,
                IsManager = false,
                
            };
            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "burak.123");

            var appUser2 = new AppUser
            {
                Id = 2,
                CompanyId = 1,
                Email = "alitopluu@gmail.com",
                NormalizedEmail = "alitopluu@gmail.com",
                EmailConfirmed = true,
                UserName = "alitopluu@gmail.com",
                NormalizedUserName = "ALITOPLUU@GMAIL.COM",
                Address = "Atakum/SAMSUN",
                Name = "Ali",
                Surname = "TOPLU",
                Gender = Gender.Male,
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                Status = Status.Active,
                SecurityStamp = "dfhfdgh",
                FirstLogin = true,
                WorkingSituation = true,
                IsManager = false
            };
            appUser2.PasswordHash = ph.HashPassword(appUser2, "ali.123");

            var appUser3 = new AppUser
            {
                Id = 3,
                CompanyId = 1,
                Email = "ugras.ulascan@hotmail.com",
                NormalizedEmail = "ugras.ulascan@hotmail.com",
                EmailConfirmed = true,
                UserName = "ugras.ulascan@hotmail.com",
                NormalizedUserName = "UGRAS.ULASCAN@HOTMAIL.COM",
                Address = "ANKARA",
                Name = "Ulaşcan",
                Surname = "UĞRAŞ",
                Gender = Gender.Male,
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                Status = Status.Active,
                SecurityStamp = "sfsdfsdf",
                FirstLogin = true,
                WorkingSituation = true,
                IsManager = true
            };
            appUser3.PasswordHash = ph.HashPassword(appUser3, "ulas.123");

            var appUser4 = new AppUser
            {
                Id = 4,
                CompanyId = 1,
                Email = "handebalaban92@gmail.com",
                NormalizedEmail = "handebalaban92@gmail.com",
                EmailConfirmed = true,
                UserName = "handebalaban92@gmail.com",
                NormalizedUserName = "HANDEBALABAN92@GMAIL.COM",
                Address = "İstanbul",
                Name = "Hande",
                Surname = "BALABAN",
                Gender = Gender.Female,
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                Status = Status.Active,
                SecurityStamp = "sdfsdf",
                FirstLogin = true,
                WorkingSituation = true,
                IsManager = false
            };
            appUser4.PasswordHash = ph.HashPassword(appUser4, "hande.123");

            builder.Entity<Company>().HasData(company);
            builder.Entity<AppRole>().HasData(appRole, appRole2, appRole3);
            builder.Entity<AppUser>().HasData(appUser, appUser2, appUser3, appUser4);

            base.OnModelCreating(builder);
        }
    }
}
