using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebApplication1.Contracts;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class ArrivalController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IArrivalService _arrivalService;

        public ArrivalController(ITokenService tokenService,IArrivalService arrivalService)
        {
            _tokenService = tokenService;
            _arrivalService = arrivalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArrivalData()
        {
            List<EmployeeArrivalFullInfoDTO> dtos = await _arrivalService.GetAllAsync();

            List<EmployeeArrivalViewModel> models = MapModels(dtos);

            return View(models);
        }

        [HttpPost]
        
        public async Task<IActionResult> PostArrivalData([FromBody]List<EmployeeArrivalDTO> dtos)
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
            return dtos.Select(d => new EmployeeArrivalViewModel()
            {
                Role = d.Employee.Role.Name,
                Teams = d.Employee.Teams.Select(t => t.Name).ToHashSet(),
                Surname = d.Employee.Surname,
                Forename = d.Employee.Forename,
                ManagerId = d.Employee.ManagerId,
                EmployeeId = d.Employee.Id,
                ArrivalTime = d.ArrivalTime
            })
                .ToList();
        }
    }
}
