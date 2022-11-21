using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Data.Entities
{
    public class Team
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual HashSet<Employee> Employees { get; set; }
    }
}
