using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Boat;

namespace VehicleRentingSystem.Services
{
    public class BoatService : IBoatService
    {
        public Task AddBoatAsync(BoatViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddBoatToCollectionAsync(int boatId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BoatViewModel>> GetAllBoatAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BoatViewModel>> GetRentedAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBoatFromCollectionAsync(int boatId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
