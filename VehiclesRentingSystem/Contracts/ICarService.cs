using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Car;

namespace VehicleRentingSystem.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarViewModel>> GetAllCarAsync();

        Task<IEnumerable<CarType>> GetCarTypesAsync();

        Task AddCarAsync(AddCarViewModel model);

        Task AddCarToCollectionAsync(int carId, string userId);

        Task<IEnumerable<CarViewModel>> GetRentedAsync(string userId);

        Task RemoveCarFromCollectionAsync(int carId, string userId);

        Task DeleteCarAsync(int carId);
    }
}
