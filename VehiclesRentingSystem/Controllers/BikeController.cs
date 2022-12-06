using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Controllers
{
    public class BikeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
