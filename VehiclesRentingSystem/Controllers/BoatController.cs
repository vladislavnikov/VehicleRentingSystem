using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Controllers
{
    public class BoatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
