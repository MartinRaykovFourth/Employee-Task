using Microsoft.AspNetCore.Mvc;

namespace EmployeeArrivalApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}