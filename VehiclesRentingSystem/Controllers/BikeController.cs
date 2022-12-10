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
        public async Task<IActionResult> All()
        {
            var model = await bikeService.GetAllBikeAsync();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddBikeViewModel() { };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBikeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bikeService.AddBikeAsync(model); //check if it is addbikevm or just bikevm
                return RedirectToAction(nameof(All)); //AllBike
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

            return RedirectToAction(nameof(Rented));
        }

        public async Task<IActionResult> Rented()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await bikeService.GetRentedAsync(userId);

            return View("Mine", model); //Mine, Bikes
        }

        public async Task<IActionResult> RemoveBikeFromCollection(int bikeId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await bikeService.RemoveBikeFromCollectionAsync(bikeId, userId);

            return RedirectToAction(nameof(Rented));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int bikeId)
        {
            var bikes = await bikeService.GetAllBikeAsync();

            var bike = bikes.FirstOrDefault(c => c.Id == bikeId);


            return View("Detail", bike);
        }

    }
}
