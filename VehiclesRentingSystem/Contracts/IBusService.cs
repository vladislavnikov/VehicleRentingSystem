using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Bus;


namespace VehicleRentingSystem.Contracts
{
    public interface IBusService
    {
        Task<IEnumerable<BusViewModel>> GetAllBusAsync();

        Task AddBusAsync(AddBusViewModel model);

        Task AddBusToCollectionAsync(int busId, string userId);

        Task<IEnumerable<BusViewModel>> GetRentedAsync(string userId);

        Task RemoveBusFromCollectionAsync(int busId, string userId);
    }
}
