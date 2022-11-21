using EmployeesApp.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesApp.DTOs
{
    public class EmployeeJsonDto
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string[] Teams { get; set; }
    }
}
