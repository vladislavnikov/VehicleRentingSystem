﻿using VehicleRentingSystem.Models.Bike;

namespace VehicleRentingSystem.Contracts
{
    public interface IBikeService
    {
        Task<IEnumerable<BikeViewModel>> GetAllBikeAsync();

        Task AddBikeAsync(BikeViewModel model);

        Task AddBikeToCollectionAsync(int bikeId, string userId);

        Task<IEnumerable<BikeViewModel>> GetRentedAsync(string userId);

        Task RemoveBikeFromCollectionAsync(int bikeId, string userId);
    }
}
