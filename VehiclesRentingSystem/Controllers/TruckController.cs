using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Trucks;
using VehicleRentingSystem.Services;

namespace VehicleRentingSystem.Controllers
{
    [Authorize]
    public class TruckController : Controller
    {
        private readonly ITruckService truckService;

        public TruckController(ITruckService _truckService)
        {
            this.truckService = _truckService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await truckService.GetAllTruckAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddTruckViewModel() { };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTruckViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await truckService.AddTruckAsync(model); //check if it is addbikevm or just bikevm
                return RedirectToAction(nameof(All)); //All
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddTruckToCollection(int truckId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await truckService.AddTruckToCollectionAsync(truckId, userId);
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
            var model = await truckService.GetRentedAsync(userId);

            return View("Mine", model); //Mine, Bikes
        }

        public async Task<IActionResult> RemoveTruckFromCollection(int truckId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await truckService.RemoveTruckFromCollectionAsync(truckId, userId);

            return RedirectToAction(nameof(Rented));
        }

    }
}
