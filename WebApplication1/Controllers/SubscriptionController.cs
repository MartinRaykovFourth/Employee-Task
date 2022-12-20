using Microsoft.AspNetCore.Mvc;
using EmployeeArrivalApp.DataAccess.Contracts;

namespace EmployeeArrivalApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private string apiUrl;
        private readonly ITokenService _tokenService;
        private readonly ISubscriptionService _httpService;

        public SubscriptionController(ITokenService tokenService, ISubscriptionService httpService,IConfiguration conf)
        {
            _tokenService = tokenService;
            _httpService = httpService;
            apiUrl = conf.GetValue<string>("ApiURL");
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
