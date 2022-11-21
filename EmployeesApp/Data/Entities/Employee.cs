using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesApp.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set;}
        public virtual Role Role { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }

        public virtual HashSet<Team> Teams { get; set; }
    }
}
