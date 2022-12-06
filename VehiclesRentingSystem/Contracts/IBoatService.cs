﻿using VehicleRentingSystem.Models.Boat;

namespace VehicleRentingSystem.Contracts
{
    public interface IBoatService
    {
        Task<IEnumerable<BoatViewModel>> GetAllBoatAsync();

        Task AddBoatAsync(BoatViewModel model);

        Task AddBoatToCollectionAsync(int boatId, string userId);

        Task<IEnumerable<BoatViewModel>> GetRentedAsync(string userId);

        Task RemoveBoatFromCollectionAsync(int boatId, string userId);
    }
}
