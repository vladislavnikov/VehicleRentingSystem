using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRentingSystem.Contracts;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TruckController : Controller
    {
        private readonly ITruckService truckService;

        public TruckController(ITruckService _truckService)
        {
            this.truckService = _truckService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await truckService.GetAllTruckAsync();

            return View(models);
        }
    }
}
