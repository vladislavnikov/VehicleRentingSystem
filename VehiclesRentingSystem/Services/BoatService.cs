using Microsoft.EntityFrameworkCore;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Boat;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Services
{
    public class BoatService : IBoatService
    {
        private readonly VehicleDbContext context;

        public BoatService(VehicleDbContext _context)
        {
            context = _context;
        }

        public async Task AddBoatAsync(AddBoatViewModel model)
        {
            var boat = new Boat()
            {
                Brand = model.Brand,
                PricePerHour = model.PricePerHour,
                ImageUrl = model.ImageUrl,
                Power = model.Power
            };

            await context.Boats.AddAsync(boat);
            await context.SaveChangesAsync();
        }

        public async Task AddBoatToCollectionAsync(int boatId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersBoats)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var boat = await context.Boats.FirstOrDefaultAsync(b => b.Id == boatId);

            if (boat == null)
            {
                throw new ArgumentException("Invalid BoatID");
            }

            if (!user.UsersBoats.Any(b => b.BoatId == boatId))
            {
                user.UsersBoats.Add(new UserBoat()
                {
                    BoatId = boat.Id,
                    UserId = user.Id,
                    Boat = boat,
                    User = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteBoatAsync(int boatId)
        {
            var boat = context.Boats.FirstOrDefault(b => b.Id == boatId);
            context.Boats.Remove(boat);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BoatViewModel>> GetAllBoatAsync()
        {
            var boats = await context.Boats.ToListAsync();

            return boats.Select(b => new BoatViewModel()
            {
                Id = b.Id,
                Brand = b.Brand,
                PricePerHour = b.PricePerHour,
                ImageUrl = b.ImageUrl,
                Power = b.Power
            });
        }

        public async Task<IEnumerable<BoatViewModel>> GetRentedAsync(string userId)
        {
            var user = await context.Users
                 .Where(u => u.Id == userId)
                 .Include(u => u.UsersBoats)
                 .ThenInclude(b => b.Boat)
                 .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            return user.UsersBoats.Select(c => new BoatViewModel()
            {
                Id = c.Boat.Id,
                Brand = c.Boat.Brand,
                PricePerHour = c.Boat.PricePerHour,
                ImageUrl = c.Boat.ImageUrl,
                Power = c.Boat.Power
            });
        }

        public async Task RemoveBoatFromCollectionAsync(int boatId, string userId)
        {
            var user = await context.Users
              .Where(u => u.Id == userId)
              .Include(u => u.UsersBoats)
              .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid UserID");
            }

            var boat = user.UsersBoats
                .FirstOrDefault(c => c.BoatId == boatId);

            if (boat != null)
            {
                user.UsersBoats.Remove(boat);

                await context.SaveChangesAsync();
            }
        }
    }
}
