using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    public class TruckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
