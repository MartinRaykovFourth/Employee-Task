namespace EmployeeArrivalApp.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public HashSet<Team> Teams { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}