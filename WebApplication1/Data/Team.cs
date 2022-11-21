namespace WebApplication1.Data
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HashSet<Employee> Employees { get; set; }
    }
}