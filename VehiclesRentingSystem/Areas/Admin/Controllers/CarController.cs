using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
