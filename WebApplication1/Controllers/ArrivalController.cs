using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using EmployeeArrivalApp.DTOs;
using EmployeeArrivalApp.Models;
using EmployeeArrivalApp.DataAccess.Contracts;
using EmployeeArrivalApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeArrivalApp.Controllers
{
    public class ArrivalController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IArrivalService _arrivalService;
        private readonly IHubContext<EmployeeArrivalHub> _hub;

        public ArrivalController(ITokenService tokenService, IArrivalService arrivalService,IHubContext<EmployeeArrivalHub> hub)
        {
            _tokenService = tokenService;
            _arrivalService = arrivalService;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GetArrivalData(string nameSearchCriteria, string sortByCriteria)
        {
            List<EmployeeArrivalFullInfoDTO> dtos = await _arrivalService.GetAllAsync();

            if (sortByCriteria != null)
            {
                dtos = SortEmployees(sortByCriteria, dtos);
            }

            if (nameSearchCriteria != null)
            {
                nameSearchCriteria = nameSearchCriteria.ToLower();
                dtos = dtos
                    .Where(d => d.Employee.Surname.ToLower().StartsWith(nameSearchCriteria))
                    .ToList();
            }

            List<EmployeeArrivalViewModel> models = MapModels(dtos);

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> PostArrivalData([FromBody] List<EmployeeArrivalDTO> dtos)
        {
            StringValues coll;
            Request.Headers.TryGetValue("X-Fourth-Token", out coll);
            string token = coll[0];

            if (await _tokenService.ValidateToken(token))
            {
                await _arrivalService.AddToDatabaseAsync(dtos);
            }
            else
            {
                return Unauthorized();
            }

            if (dtos != null && dtos.Count > 0)
            {
                List<EmployeeArrivalFullInfoDTO> modelsDTOs = await _arrivalService.GetNewestArrivalsAsync(dtos.Count);
                List<EmployeeArrivalViewModel> arrivals = MapArrivals(modelsDTOs);

                await _hub.Clients.All.SendAsync("RecievedArrivals", arrivals);

                return Ok();
            }
           
            return BadRequest();
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

        private static List<EmployeeArrivalViewModel> MapModels(List<EmployeeArrivalFullInfoDTO> dtos)
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
        private static List<EmployeeArrivalFullInfoDTO> SortEmployees(string sortByCriteria, List<EmployeeArrivalFullInfoDTO> dtos)
        {
            switch (sortByCriteria)
            {
                case "Role":
                    dtos = dtos
                        .OrderBy(d => d.Employee.Role)
                        .ToList();
                    break;
                case "Manager Id":
                    dtos = dtos
                        .OrderBy(d => d.Employee.ManagerId)
                       .ToList();
                    break;
                case "Employee Id":
                    dtos = dtos
                        .OrderBy(d => d.Employee.EmployeeId)
                       .ToList();
                    break;
            }

            return dtos;
        }

    }
}
