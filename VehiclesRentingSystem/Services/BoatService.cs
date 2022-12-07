using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Models.Boat;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Services
{
    public class BoatService : IBoatService
    {
        private readonly VehicleDbContext context;

        public BikeService(VehicleDbContext _context)
        {
            context = _context;
        }

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
