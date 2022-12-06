using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Controllers
{
    public class TruckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
