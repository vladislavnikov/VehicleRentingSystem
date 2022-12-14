using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Car;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BikeController : Controller
    {
        private readonly IBikeService bikeService;

        public BikeController(IBikeService _bikeService)
        {
            this.bikeService= _bikeService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await bikeService.GetAllBikeAsync();

            return View(models);
        }
    }
}
