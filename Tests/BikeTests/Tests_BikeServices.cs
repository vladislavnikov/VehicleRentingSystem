using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Bike;
using VehicleRentingSystem.Models.Car;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Tests.BikeTests
{
    [TestFixture]
    public class Tests_BikeServices
    {
        private IEnumerable<Bike> bikeList;
        private VehicleDbContext context;

        [SetUp]
        public void TestInitialize()
        { 
         this.bikeList = new List<Bike>() 
         { 
          new Bike(){Id=1, Brand = "Trek",PricePerHour = 20, ImageUrl ="trek.jpg" },
          new Bike(){Id=2, Brand = "Giant",PricePerHour = 25, ImageUrl ="giant.jpg" },
          new Bike(){Id=3, Brand = "YT",PricePerHour = 30, ImageUrl ="yt.jpg" }
         };

            var options = new DbContextOptionsBuilder<VehicleDbContext>()
                   .UseInMemoryDatabase(databaseName: "VehiclesInMemoryDb")
                   .Options;
            this.context = new VehicleDbContext(options);
            this.context.AddRangeAsync(this.bikeList);
            this.context.SaveChangesAsync();
        }

        [Test]
        public async Task Test_DeleteBikes()
        {
            int bikeId = 1;

            IBikeService service =
                new BikeService(context);

            await service.DeleteBikeAsync(bikeId);
            await context.SaveChangesAsync();

            var dbBike = context.Bikes.ToList()
                .Find(c => c.Id == bikeId);

            Assert.True(dbBike == null);
        }

        [Test]
        public async Task Test_AddBike()
        {
            AddBikeViewModel car = new AddBikeViewModel()
            {
                Brand = "SC",
                PricePerHour = 45,
                ImageUrl = "sc.jpg"
            };

            IBikeService service =
                new BikeService(context);

            await service.AddBikeAsync(car);
            await context.SaveChangesAsync();

            var dbBikes = context.Bikes.ToList();

            Assert.True(dbBikes.Count() == 4);
        }

        [Test]
        public async Task Test_GetAllBikes()
        {
            IBikeService service =
                new BikeService(context);

            var dbBikes = await service.GetAllBikeAsync();

            Assert.True(dbBikes.Count() == 3);
        }
    }
}
