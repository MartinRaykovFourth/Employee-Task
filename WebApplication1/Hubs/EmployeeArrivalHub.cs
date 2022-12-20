using EmployeeArrivalApp.DataAccess.Contracts;
using EmployeeArrivalApp.DTOs;
using EmployeeArrivalApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeArrivalApp.Hubs
{
    public class EmployeeArrivalHub : Hub
    {
        private readonly IArrivalService _arrivalService;

        public EmployeeArrivalHub(IArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }

        public async Task SendArrivals()
        {
            List<EmployeeArrivalFullInfoDTO> dtos = await _arrivalService.GetAllAsync();
            List<EmployeeArrivalViewModel> arrivals = MapArrivals(dtos);

            await Clients.All.SendAsync("RecievedArrivals", arrivals);
        }

        private static List<EmployeeArrivalViewModel> MapArrivals(List<EmployeeArrivalFullInfoDTO> dtos)
        {
            return dtos
                .Select(d => new EmployeeArrivalViewModel()
                {
                    Role = d.Employee.Role,
                    Teams = d.Employee.Teams,
                    Surname = d.Employee.Surname,
                    Forename = d.Employee.Forename,
                    ManagerId = d.Employee.ManagerId,
                    EmployeeId = d.Employee.EmployeeId,
                    ArrivalTime = d.ArrivalTime
                })
                .ToList();
        }
    }
}
