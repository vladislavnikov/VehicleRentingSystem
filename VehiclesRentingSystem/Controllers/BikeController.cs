using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Car;
using VehicleRentingSystem.Services;

namespace VehicleRentingSystem.Controllers
{
    [Authorize]
    public class BikeController : Controller
    {
        private readonly IBikeService bikeService;

        public BikeController(IBikeService _bikeService)
        {
            bikeService = _bikeService;
        }

        [HttpGet]
        public async Task<IActionResult> AllBike()
        {
            var model = await bikeService.GetAllBikeAsync();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddBike()
        {
            var model = new AddBikeViewModel() { };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBike(AddBikeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
               await bikeService.AddBikeAsync(model); //check if it is addbikevm or just bikevm
                return RedirectToAction(nameof(AllBike)); //AllBike
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddBikeToCollection(int bikeId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await bikeService.AddBikeToCollectionAsync(bikeId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(AllBike)); //RentedBikes
        }

        public async Task<IActionResult> RentedBikes()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await bikeService.GetRentedAsync(userId);

            return View("Mine", model); //MineBikes
        }

        public async Task<IActionResult> RemoveBikeFromCollection(int bike)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await bikeService.RemoveBikeFromCollectionAsync(bike, userId);

            return RedirectToAction(nameof(RentedBikes)); 
        }


    }
}
