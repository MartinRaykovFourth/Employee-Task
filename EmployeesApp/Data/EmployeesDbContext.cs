using EmployeesApp.Data.Entities;
using EmployeesApp.DTOs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace EmployeesApp
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext()
        {
        }

        //public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<Team> Teams { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-C32A1UF\\SQLEXPRESS;Database=EmployeesApp;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string jsonString = await File.ReadAllTextAsync("Data/employees.json");
            EmployeeJsonDto[] dtos = JsonConvert.DeserializeObject<EmployeeJsonDto[]>(jsonString);

            SeedTeams(modelBuilder, dtos);
            SeedRoles(modelBuilder, dtos);
            SeedEmployees(modelBuilder, dtos);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedTeams(ModelBuilder builder, EmployeeJsonDto[] dtos)
        {
            var teams = dtos
                 .SelectMany(x => x.Teams)
                 .GroupBy(x => x)
                 .Select((x, i) => new Team { Id = i + 1, Name = x.First() });

            builder.Entity<Team>().HasData(teams);
        }

        private void SeedRoles(ModelBuilder builder, EmployeeJsonDto[] dtos)
        {
            HashSet<Role> roles = new HashSet<Role>();

            foreach (var d in dtos)
            {
                if (!roles.Any(role => role.Name == d.Role))
                {
                    roles.Add(new Role()
                    {
                        Name = d.Role
                    });
                }
            }

            builder.Entity<Role>().HasData(roles);
        }

        private void SeedEmployees(ModelBuilder builder, EmployeeJsonDto[] dtos)
        {
            List<Employee> employees = new List<Employee>();

            foreach (var d in dtos)
            {
                employees.Add(new Employee()
                {
                    Age = d.Age,
                    Email = d.Email,
                    Role = this.Roles
                    .Where(r => r.Name == d.Role)
                    .Single(),
                    Id = d.Id,
                    ManagerId = d.ManagerId,
                    Name = d.Name,
                    SurName = d.SurName,
                    Teams = this.Teams
                    .Where(t => d.Teams.Any(dto => dto == t.Name))
                    .ToHashSet()
                });
            }

            builder.Entity<Employee>().HasData(employees);
        }
    }
}
