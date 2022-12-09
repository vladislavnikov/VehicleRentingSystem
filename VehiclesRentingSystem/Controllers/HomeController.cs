using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehiclesRentingSystem.Models;

namespace VehiclesRentingSystem.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                // return RedirectToAction("All", "Car");
                return View();
            }
            //
            return View();
        }

    }
}