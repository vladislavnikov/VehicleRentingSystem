using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Boat;
using VehicleRentingSystem.Models.Car;
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
        [Area("Admin")]
        public async Task<IActionResult> Add()
        {
            var model = new AddBoatViewModel() { };

            return View(model);
        }

        [HttpPost]
        [Area("Admin")]
        public async Task<IActionResult> Add(AddBoatViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await boatService.AddBoatAsync(model);
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
        public async Task<IActionResult> Delete(int boatId)
        {

            await boatService.DeleteBoatAsync(boatId);
            return RedirectToAction(nameof(All));
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

        [HttpPost]
        public async Task<IActionResult> Detail(int boatId)
        {
            var boats = await boatService.GetAllBoatAsync();

            var boat = boats.FirstOrDefault(c => c.Id == boatId);


            return View("Detail", boat);
        }
    }
}
