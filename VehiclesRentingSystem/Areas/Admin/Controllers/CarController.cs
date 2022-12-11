using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Car;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly VehicleDbContext context;
        private readonly CarService carService;

        public CarController(VehicleDbContext _context, CarService _carService)
        {
            context = _context;
            carService = _carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // var model = await carService.GetAllCarAsync();

            var models = context.Cars
                .Select(c => new CarViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Power = c.Power,
                    PricePerHour = c.PricePerHour
                });

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddCarViewModel()
            {
                CarTypes = await carService.GetCarTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarViewModel model)
        {
            var car = new Car
            {
                Brand = model.Brand,
                Power = model.Power,
                PricePerHour = model.PricePerHour,
                CarTypeId = model.CarTypeId,
            };

            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int carId)
        {

            var car = context.Cars.FirstOrDefault(c => c.Id == carId);
            context.Cars.Remove(car);
            return RedirectToAction(nameof(Index));
        }
    }
}
