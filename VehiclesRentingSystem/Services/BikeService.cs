using Microsoft.EntityFrameworkCore;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Car;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Services
{
    public class BikeService : IBikeService
    {
        private readonly VehicleDbContext context;

        public BikeService(VehicleDbContext _context)
        {
            context = _context;
        }

        public async Task AddBikeAsync(BikeViewModel model)
        {
            var bike = new Bike()
            {
                Brand = model.Brand,
                PricePerHour = model.PricePerHour,
                ImageUrl = model.ImageUrl
            };

            await context.Bikes.AddAsync(bike);
            await context.SaveChangesAsync();
        }

        public async Task AddBikeToCollectionAsync(int bikeId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersBikes)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var bike = await context.Bikes.FirstOrDefaultAsync(b => b.Id == bikeId);

            if (bike == null)
            {
                throw new ArgumentException("Invalid CarID");
            }

            if (!user.UsersBikes.Any(b => b.BikeId == bikeId))
            {
                user.UsersBikes.Add(new UserBike()
                {
                    BikeId = bike.Id,
                    UserId = user.Id,
                    Bike = bike,
                    User = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BikeViewModel>> GetAllBikeAsync()
        {
            var bikes = await context.Bikes.ToListAsync();

            return bikes.Select(b => new BikeViewModel()
            {
                Id = b.Id,
                Brand = b.Brand,
                PricePerHour = b.PricePerHour,
                ImageUrl = b.ImageUrl
            });
        }

        public async Task<IEnumerable<BikeViewModel>> GetRentedAsync(string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersBikes)
               .ThenInclude(b => b.Bike)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            return user.UsersBikes.Select(c => new BikeViewModel()
            {
                Brand = c.Bike.Brand,
                PricePerHour = c.Bike.PricePerHour,
                ImageUrl = c.Bike.ImageUrl
            });
        }

        public async Task RemoveBikeFromCollectionAsync(int bikeId, string userId)
        {
            var user = await context.Users
              .Where(u => u.Id == userId)
              .Include(u => u.UsersBikes)
              .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var bike = user.UsersBikes
                .FirstOrDefault(c => c.BikeId == bikeId);

            if (bike != null)
            {
                user.UsersBikes.Remove(bike);

                await context.SaveChangesAsync();
            }
        }
    }
}
