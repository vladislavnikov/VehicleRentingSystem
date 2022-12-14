using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Bus;
using VehicleRentingSystem.Services;

namespace VehicleRentingSystem.Controllers
{
    [Authorize]
    public class BusController : Controller
    {
        private readonly IBusService busService;

        public BusController(IBusService _busService)
        {
            busService = _busService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await busService.GetAllBusAsync();

            return View(model);
        }

        [HttpGet]
        [Area("Admin")]
        public async Task<IActionResult> Add()
        {
            var model = new AddBusViewModel(){ };

            return View(model);
        }

        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> Add(AddBusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await busService.AddBusAsync(model); //check if it is addbikevm or just bikevm
                return RedirectToAction(nameof(All)); //AllBike
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }

        }

        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> Delete(int busId)
        {

            await busService.DeleteBusAsync(busId);
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddBusToCollection(int busId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await busService.AddBusToCollectionAsync(busId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Rented));
        }

        public async Task<IActionResult> Rented()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await busService.GetRentedAsync(userId);

            return View("Mine", model); //Mine, Bus
        }

        public async Task<IActionResult> RemoveBusFromCollection(int busId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await busService.RemoveBusFromCollectionAsync(busId, userId);

            return RedirectToAction(nameof(Rented));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int busId)
        {
            var cars = await busService.GetAllBusAsync();

            var car = cars.FirstOrDefault(c => c.Id == busId);


            return View("Detail", car);
        }
    }
}
