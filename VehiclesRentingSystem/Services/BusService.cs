using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Bus;

namespace VehicleRentingSystem.Services
{
    public class BusService : IBusService
    {
        public Task AddBusAsync(BusViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddBusToCollectionAsync(int busId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BusViewModel>> GetAllBusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BusViewModel>> GetRentedAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBusFromCollectionAsync(int busId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
