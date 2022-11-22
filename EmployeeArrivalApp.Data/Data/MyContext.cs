using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmployeeArrivalApp.Data
{
    public class MyContext : DbContext
    {
        public MyContext()
        { 
        }
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeArrival> EmployeesArrivals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var employeesJson = JsonConvert.DeserializeObject<List<EmployeeJsonObject>>(File.ReadAllText("../EmployeeArrivalApp.Data/data/employees.json"));

            var teams = employeesJson
                .SelectMany(x => x.Teams)
                .GroupBy(x => x)
                .Select((x, i) => new Team { Id = i + 1, Name = x.First() });

            var roles = employeesJson
                .Select(x => x.Role)
                .GroupBy(x => x)
                .Select((x, i) => new Role { Id = i + 1, Name = x.First() });

            var employees = employeesJson
                .Select(x => new Employee
                {
                    Id = x.Id + 1,
                    Surname = x.Name,
                    Forename = x.SurName,
                    RoleId = roles.Single(y => y.Name == x.Role).Id,
                    ManagerId = x.ManagerId.HasValue ? x.ManagerId + 1 : null
                });

            var employeeTeams = employeesJson
                .SelectMany(x => x.Teams.Select(team => (x.Id, team)))
                .Select(x => new { EmployeesId = x.Id, TeamsId = teams.Single(z => z.Name == x.team).Id })
                .ToHashSet();



            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<Employee>().HasData(employees);

            modelBuilder
                .Entity<Employee>()
                .HasMany(x => x.Teams)
                .WithMany(x => x.Employees)
                .UsingEntity(j => j
                .ToTable("EmployeeTeams")
                .HasData(employeeTeams));

            base.OnModelCreating(modelBuilder);
        }
    }
}
