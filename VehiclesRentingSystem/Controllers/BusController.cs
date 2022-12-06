using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Controllers
{
    public class BusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
