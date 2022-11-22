namespace WebApplication1.DTOs
{
    public class EmployeeArrivalEmployeeInfoDTO
    {
        public string Role { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public int? ManagerId { get; set; }
        public int EmployeeId { get; set; }
        public HashSet<string> Teams { get; set; }
        
    }
}
