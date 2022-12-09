using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Boat;
using VehicleRentingSystem.Services;

namespace VehicleRentingSystem.Controllers
{
    [Authorize]
    public class BoatController : Controller
    {
        private readonly IBoatService boatService;

        public BoatController(IBoatService _boatService)
        {
            boatService = _boatService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await boatService.GetAllBoatAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddBoatViewModel() { };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBoatToCollection(int boatId)
        {
            try
            {
                var userId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await boatService.AddBoatToCollectionAsync(boatId, userId);
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
            var model = await boatService.GetRentedAsync(userId);

            return View("Mine", model); //Mine, Boat
        }

        public async Task<IActionResult> RemoveBoatFromCollection(int boatId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await boatService.RemoveBoatFromCollectionAsync(boatId,userId);

            return RedirectToAction(nameof(Rented));
        }
    }
}
