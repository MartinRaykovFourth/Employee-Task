using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public class ArrivalService : IArrivalService
    {
        private readonly MyContext _context;
        public ArrivalService(MyContext context)
        {
            _context = context;
        }

        public async Task AddToDatabaseAsync(List<EmployeeArrivalDTO> dtos)
        {
            List<EmployeeArrival> arrivals = dtos
                .Select(d => new EmployeeArrival()
                {
                    EmployeeId = d.EmployeeId,
                    ArrivalTime = d.When
                })
                .ToList();

            _context.EmployeesArrivals.AddRange(arrivals);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeArrivalFullInfoDTO>> GetAllAsync()
        {
            return await _context.EmployeesArrivals
            .Select(ea => new EmployeeArrivalFullInfoDTO()
            {
                ArrivalTime = ea.ArrivalTime,
                Employee = ea.Employee
            })
            .ToListAsync();
        }
    }
}
