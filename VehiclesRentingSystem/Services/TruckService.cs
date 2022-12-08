using Microsoft.EntityFrameworkCore;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Trucks;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Services
{
    public class TruckService : ITruckService
    {
        private readonly VehicleDbContext context;

        public TruckService(VehicleDbContext _context)
        {
            context = _context;
        }

        public async Task AddTruckAsync(AddTruckViewModel model)
        {
            var truck = new Truck()
            {
                Brand = model.Brand,
                Power = model.Power,
                PricePerHour = model.PricePerHour,
                MaxWeight = model.MaxWeight,
                ImageUrl = model.ImageUrl
            };

            await context.Trucks.AddAsync(truck);
            await context.SaveChangesAsync();
        }

        public async Task AddTruckToCollectionAsync(int truckId, string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersTrucks)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var truck = await context.Trucks.FirstOrDefaultAsync(b => b.Id == truckId);

            if (truck == null)
            {
                throw new ArgumentException("Invalid CarID");
            }

            if (!user.UsersTrucks.Any(b => b.TruckId == truckId))
            {
                user.UsersTrucks.Add(new UserTruck()
                {
                    TruckId = truck.Id,
                    UserId = user.Id,
                    Truck = truck,
                    User = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TruckViewModel>> GetAllTruckAsync()
        {
            var trucks = await context.Trucks.ToListAsync();

            return trucks.Select(t => new TruckViewModel()
            {
                Id = t.Id,
                Brand = t.Brand,
                Power = t.Power,
                PricePerHour = t.PricePerHour,
                MaxWeight = t.MaxWeight,
                ImageUrl = t.ImageUrl
            });
        }

        public async Task<IEnumerable<TruckViewModel>> GetRentedAsync(string userId)
        {
            var user = await context.Users
              .Where(u => u.Id == userId)
              .Include(u => u.UsersTrucks)
              .ThenInclude(t => t.Truck)
              .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            return user.UsersTrucks.Select(c => new TruckViewModel()
            {
                Id = c.TruckId, 
                Brand = c.Truck.Brand,
                PricePerHour = c.Truck.PricePerHour,
                ImageUrl = c.Truck.ImageUrl,
                MaxWeight = c.Truck.MaxWeight,
                Power = c.Truck.Power
            });
        }

        public async Task RemoveTruckFromCollectionAsync(int truckId, string userId)
        {
            var user = await context.Users
             .Where(u => u.Id == userId)
             .Include(u => u.UsersTrucks)
             .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var truck = user.UsersTrucks
                .FirstOrDefault(c => c.TruckId == truckId);

            if (truck != null)
            {
                user.UsersTrucks.Remove(truck);

                await context.SaveChangesAsync();
            }
        }
    }
}