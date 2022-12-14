using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRentingSystem.Contracts;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BoatController : Controller
    {
        private readonly IBoatService boatService;

        public BoatController(IBoatService _boatService)
        {
            this.boatService = _boatService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await boatService.GetAllBoatAsync();

            return View(models);
        }
    }
}
