using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleRentingSystem.Data.Models;

namespace VehiclesRentingSystem.Data
{
    public class VehicleDbContext : IdentityDbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Boat> Boats { get; set; }

        public DbSet<Bus> Buses { get; set; }

        public DbSet<Truck> Trucks { get; set; }

        public DbSet<Bike> Bikes { get; set; }

        public DbSet<CarType> Types { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserCar>()
                .HasKey(x => new { x.UserId, x.CarId });

            builder.Entity<UserBus>()
               .HasKey(x => new { x.UserId, x.BusId });

            builder.Entity<UserBoat>()
               .HasKey(x => new { x.UserId, x.BoatId });

            builder.Entity<UserTruck>()
               .HasKey(x => new { x.UserId, x.TruckId });

            builder.Entity<UserBike>()
               .HasKey(x => new { x.UserId, x.BikeId });

            builder
               .Entity<CarType>()
                        .HasData(new CarType()
                        {
                            Id = 1,
                            Name = "Sedan"
                        },
                        new CarType()
                        {
                            Id = 2,
                            Name = "Coupe"
                        },
                        new CarType()
                        {
                            Id = 3,
                            Name = "HatchBack"
                        },
                        new CarType()
                        {
                            Id = 4,
                            Name = "Kombi"
                        },
                        new CarType()
                        {
                            Id = 5,
                            Name = "Cabrio"
                        });

            base.OnModelCreating(builder);
        }
    }
}