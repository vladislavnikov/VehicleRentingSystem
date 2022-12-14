using Microsoft.AspNetCore.Mvc;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    public class BusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
