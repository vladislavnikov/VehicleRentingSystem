using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Trucks;

namespace VehicleRentingSystem.Contracts
{
    public interface ITruckService
    {
        Task<IEnumerable<TruckViewModel>> GetAllTruckAsync();

        Task AddTruckAsync(TruckViewModel model);

        Task AddTruckToCollectionAsync(int truckId, string userId);

        Task<IEnumerable<TruckViewModel>> GetRentedAsync(string userId);

        Task RemoveTruckFromCollectionAsync(int truckId, string userId);
    }
}
