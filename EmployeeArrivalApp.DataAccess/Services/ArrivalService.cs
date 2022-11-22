using Microsoft.EntityFrameworkCore;
using EmployeeArrivalApp.Data;
using EmployeeArrivalApp.DTOs;
using EmployeeArrivalApp.DataAccess.Contracts;

namespace EmployeeArrivalApp.DataAccess.Services
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
                Employee = new EmployeeArrivalEmployeeInfoDTO()
                {
                    EmployeeId = ea.EmployeeId,
                    Forename = ea.Employee.Forename,
                    ManagerId = ea.Employee.ManagerId,
                    Role = ea.Employee.Role.Name,
                    Surname = ea.Employee.Surname,
                    Teams = ea.Employee.Teams.Select(t => t.Name).ToHashSet()
                }
            })
            .ToListAsync();
        }
    }
}
