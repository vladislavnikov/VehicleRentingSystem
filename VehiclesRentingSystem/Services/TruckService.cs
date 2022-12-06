using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Trucks;

namespace VehicleRentingSystem.Services
{
    public class TruckService : ITruckService
    {
        public Task AddTruckAsync(TruckViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddTruckToCollectionAsync(int truckId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TruckViewModel>> GetAllTruckAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TruckViewModel>> GetRentedAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTruckFromCollectionAsync(int truckId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
