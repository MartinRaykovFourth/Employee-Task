using WebApplication1.Data;

namespace WebApplication1.DTOs
{
    public class EmployeeArrivalFullInfoDTO
    {
        public Employee Employee { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
