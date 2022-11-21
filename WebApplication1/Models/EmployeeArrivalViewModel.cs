using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class EmployeeArrivalViewModel
    {
        public int EmployeeId { get; set; }
        public int? ManagerId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public HashSet<string> Teams { get; set; }
        public string Role { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
