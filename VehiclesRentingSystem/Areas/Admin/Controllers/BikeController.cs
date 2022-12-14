using Microsoft.AspNetCore.Mvc;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Car;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    public class BikeController : Controller
    {
        private readonly VehicleDbContext context;

        public BikeController(VehicleDbContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var model = await carService.GetAllCarAsync();

            var models = context.Bikes
                .Select(c => new BikeViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    PricePerHour = c.PricePerHour,
                    ImageUrl = c.ImageUrl
                });

            return View(models);
        }
    }
}
