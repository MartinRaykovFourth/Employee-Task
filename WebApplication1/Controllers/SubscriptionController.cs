using Microsoft.AspNetCore.Mvc;
using EmployeeArrivalApp.DataAccess.Contracts;

namespace EmployeeArrivalApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private const string apiUrl = "http://localhost:51397/api/clients/subscribe?date=2016-03-10&callback=http://localhost:5025/Arrival/PostArrivalData";
        private readonly ITokenService _tokenService;
        private readonly IHttpService _httpService;

        public SubscriptionController(ITokenService tokenService, IHttpService httpService)
        {
            _tokenService = tokenService;
            _httpService = httpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscribtion()
        {
            var tokenDTO = await _httpService.GetSubscribtionAsync(apiUrl);

            if (tokenDTO != null)
            {
                await _tokenService.AddTokenToDatabaseAsync(tokenDTO);
            }

            return RedirectToAction("Index","Home");
        }

        
    }
}
