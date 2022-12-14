using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    public class BoatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
