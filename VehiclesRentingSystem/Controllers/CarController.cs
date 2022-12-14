using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Car;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly VehicleDbContext context;

        public CarController(ICarService _carService, VehicleDbContext _context)
        {
            carService = _carService;
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var fu = User;

            var model = await carService.GetAllCarAsync();

            return View(model);
        }

        [HttpGet]
        [Area("Admin")]
        //[Route("")]
        public async Task<IActionResult> Add()
        {


            var model = new AddCarViewModel()
            {
                CarTypes = await carService.GetCarTypesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> Add(AddCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await carService.AddCarAsync(model);
                 return RedirectToAction(nameof(All));
                
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }

        }

        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> Delete(int carId)
        {

            await carService.DeleteCarAsync(carId);
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int carId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await carService.AddCarToCollectionAsync(carId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Rented)); //
        }

        public async Task<IActionResult> Rented()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await carService.GetRentedAsync(userId);

            return View("Mine", model);
        }

        public async Task<IActionResult> RemoveCarFromCollection(int carId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await carService.RemoveCarFromCollectionAsync(carId, userId);

            return RedirectToAction(nameof(Rented));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int carId)
        {
            var cars = await carService.GetAllCarAsync();

            var car = cars.FirstOrDefault(c => c.Id == carId);


            return View("Detail", car);
        }



    }
}
