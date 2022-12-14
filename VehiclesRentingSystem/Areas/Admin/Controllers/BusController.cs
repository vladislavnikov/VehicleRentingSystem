using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRentingSystem.Contracts;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BusController : Controller
    {
        private readonly IBusService busService;

        public BusController(IBusService _busService)
        {
            this.busService = _busService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await busService.GetAllBusAsync();

            return View(models);
        }
    }
}
