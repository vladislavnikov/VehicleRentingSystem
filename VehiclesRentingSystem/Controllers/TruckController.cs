using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
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
        [Area("Admin")]
        public async Task<IActionResult> Add()
        {
            var model = new AddTruckViewModel() { };

            return View(model);
        }

        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> Add(AddTruckViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await truckService.AddTruckAsync(model); 
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
        public async Task<IActionResult> Delete(int truckId)
        {

            await truckService.DeleteTruckAsync(truckId);
            return RedirectToAction(nameof(All));
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

            return View("Mine", model); 
        }

        public async Task<IActionResult> RemoveTruckFromCollection(int truckId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await truckService.RemoveTruckFromCollectionAsync(truckId, userId);

            return RedirectToAction(nameof(Rented));
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int truckId)
        {
            var trucks = await truckService.GetAllTruckAsync();

            var truck = trucks.FirstOrDefault(c => c.Id == truckId);


            return View("Detail", truck);
        }
    }
}
