using Microsoft.EntityFrameworkCore;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Bus;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Services
{
    public class BusService : IBusService
    {
        private readonly VehicleDbContext context;

        public BusService(VehicleDbContext _context)
        {
            context = _context;
        }

        public async Task AddBusAsync(AddBusViewModel model)
        {
            var bus = new Bus()
            { 
            Brand = model.Brand,
            Power = model.Power,
            Seats = model.Seats,
            ImageUrl = model.ImageUrl,
            PricePerHour = model.PricePerHour
            };

            await context.Buses.AddAsync(bus);
            await context.SaveChangesAsync();
        }

        public async Task AddBusToCollectionAsync(int busId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersBuses)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var bus = await context.Buses.FirstOrDefaultAsync(b => b.Id == busId);

            if (bus == null)
            {
                throw new ArgumentException("Invalid CarID");
            }

            if (!user.UsersBuses.Any(b => b.BusId == busId))
            {
                user.UsersBuses.Add(new UserBus()
                {
                    BusId = bus.Id,
                    UserId = user.Id,
                    Bus = bus,
                    User = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BusViewModel>> GetAllBusAsync()
        {
            var buses = await context.Buses.ToListAsync();

            return buses.Select(b => new BusViewModel() { 
            Id = b.Id,
            Brand = b.Brand,
            Power = b.Power,
            Seats = b.Seats,
            PricePerHour = b.PricePerHour,
            ImageUrl = b.ImageUrl
            });
        }

        public async Task<IEnumerable<BusViewModel>> GetRentedAsync(string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersBuses)
               .ThenInclude(b => b.Bus)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            return user.UsersBuses.Select(c => new BusViewModel()
            {
                Id = c.Bus.Id, // check in cars
                Brand = c.Bus.Brand,
                PricePerHour = c.Bus.PricePerHour,
                ImageUrl = c.Bus.ImageUrl,
                Seats = c.Bus.Seats
            });
        }

        public async Task RemoveBusFromCollectionAsync(int busId, string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersBikes)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var bus = user.UsersBuses.FirstOrDefault(b => b.BusId == busId);

            if (bus != null)
            {
                user.UsersBuses.Remove(bus);

                await context.SaveChangesAsync();
            }
        }
    }
}
