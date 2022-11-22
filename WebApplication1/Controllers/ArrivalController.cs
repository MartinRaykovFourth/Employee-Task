using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using EmployeeArrivalApp.DTOs;
using EmployeeArrivalApp.Models;
using EmployeeArrivalApp.DataAccess.Contracts;

namespace EmployeeArrivalApp.Controllers
{
    public class ArrivalController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IArrivalService _arrivalService;

        public ArrivalController(ITokenService tokenService, IArrivalService arrivalService)
        {
            _tokenService = tokenService;
            _arrivalService = arrivalService;
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

            return RedirectToAction("GetArrivalData");
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
