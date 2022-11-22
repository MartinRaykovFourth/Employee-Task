namespace EmployeeArrivalApp.Data
{
    public class EmployeeJsonObject
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public string[] Teams { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string SurName { get; set; }
    }
}