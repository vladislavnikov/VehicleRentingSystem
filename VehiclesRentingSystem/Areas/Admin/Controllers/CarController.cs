using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Car;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly VehicleDbContext context;
        private readonly ICarService carService;

        public CarController(VehicleDbContext _context, ICarService _carService)// CarService _carService)
        {
            context = _context;
            this.carService = _carService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var model = await carService.GetAllCarAsync();

            var models = context.Cars
                .Select(c => new CarViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Power = c.Power,
                    PricePerHour = c.PricePerHour,
                    ImageUrl = c.ImageUrl
                });

            return View(models);
        }

        //[HttpGet]
        ////[Area("Admin")]
        //public async Task<IActionResult> Add()
        //{

        //    var model = new AddCarViewModel()
        //    {
        //        CarTypes = await carService.GetCarTypesAsync()
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        ////[Area("Admin")]
        //public async Task<IActionResult> Add(AddCarViewModel model)
        //{
        //    var car = new Car
        //    {
        //        Brand = model.Brand,
        //        Power = model.Power,
        //        PricePerHour = model.PricePerHour,
        //        CarTypeId = model.CarTypeId,
        //        ImageUrl = model.ImageUrl
        //    };

        //    await context.Cars.AddAsync(car);
        //    await context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public IActionResult Delete(int carId)
        //{

        //    var car = context.Cars.FirstOrDefault(c => c.Id == carId);
        //    context.Cars.Remove(car);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
