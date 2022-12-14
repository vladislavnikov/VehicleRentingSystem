using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Car;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Services
{
    public class CarService : ICarService
    {
        private readonly VehicleDbContext context;

        public CarService(VehicleDbContext _context)
        {
            context = _context;
        }

        public async Task AddCarAsync(AddCarViewModel model)
        {
            var car = new Car()
            { //
            Brand = model.Brand,
            Power = model.Power,
            PricePerHour = model.PricePerHour,
            CarTypeId = model.CarTypeId,
            ImageUrl = model.ImageUrl
            };

            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
        }

        public async Task AddCarToCollectionAsync(int carId, string userId)
        {
            var user = await context.Users
                .Where(u=>u.Id == userId)
                .Include(u=>u.UsersCars)
                .FirstOrDefaultAsync();

            if (user==null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var car = await context.Cars.FirstOrDefaultAsync(c=>c.Id == carId);
            
            if (car==null)
            {
                throw new ArgumentException("Invalid CarID");
            }

            if (!user.UsersCars.Any(c=>c.CarId == carId))
            {
                user.UsersCars.Add(new UserCar()
                { 
                CarId= car.Id,
                UserId = user.Id,
                Car = car,
                User = user
                });

                await context.SaveChangesAsync();
            }

        }

        public async Task DeleteCarAsync(int carId)
        {
            var car = context.Cars.FirstOrDefault(c => c.Id == carId);
            context.Cars.Remove(car);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarViewModel>> GetAllCarAsync()
        {
            var cars = await context.Cars
                .Include(c => c.CarType).ToListAsync();

            return cars.Select(c=> new CarViewModel()
            { 
            Id = c.Id,
            Brand = c.Brand,
            Power = c.Power,
            PricePerHour = c.PricePerHour,
            ImageUrl= c.ImageUrl,
            CarType = c?.CarType?.Name
            });
        }

        public async Task<IEnumerable<CarType>> GetCarTypesAsync()
        {
            return await context.Types.ToListAsync();
        }

        public async Task<IEnumerable<CarViewModel>> GetRentedAsync(string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersCars)
                .ThenInclude(uc => uc.Car)
                .ThenInclude(c => c.CarType)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            return user.UsersCars.Select(c => new CarViewModel()
            {
                Id = c.Car.Id,
                Brand = c.Car.Brand,
                Power = c.Car.Power,
                PricePerHour = c.Car.PricePerHour,
                ImageUrl = c.Car.ImageUrl,
                CarType = c.Car.CarType?.Name
            }); ;
        }

        [HttpPost]
        public async Task RemoveCarFromCollectionAsync(int carId, string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersCars)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var car = user.UsersCars
                .FirstOrDefault(c => c.CarId == carId);

            if (car != null)
            {
                user.UsersCars.Remove(car);

                await context.SaveChangesAsync();
            }

        }
    }
}
