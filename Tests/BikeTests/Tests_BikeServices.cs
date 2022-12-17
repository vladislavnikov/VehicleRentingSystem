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
        private User user;

        [SetUp]
        public void TestInitialize()
        { 
         this.bikeList = new List<Bike>() 
         { 
          new Bike(){Id=1, Brand = "Trek",PricePerHour = 20, ImageUrl ="trek.jpg" },
          new Bike(){Id=2, Brand = "Giant",PricePerHour = 25, ImageUrl ="giant.jpg" },
          new Bike(){Id=3, Brand = "YT",PricePerHour = 30, ImageUrl ="yt.jpg" }
         };


            this.user = new User()
            {
                Id = "1",
                UserName = "test",
                Email = "test@mail.com",
                PasswordHash = "asc",
                UsersCars = new List<UserCar>(),
                UsersBikes = new List<UserBike>(),
                UsersTrucks = new List<UserTruck>(),
                UsersBoats = new List<UserBoat>(),
                UsersBuses = new List<UserBus>()
            };

            var options = new DbContextOptionsBuilder<VehicleDbContext>()
                   .UseInMemoryDatabase(databaseName: "VehiclesInMemoryDb")
                   .Options;
            this.context = new VehicleDbContext(options);
            this.context.AddRangeAsync(this.bikeList);
            this.context.AddRangeAsync(this.user);
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

        [Test]
        public async Task Test_AddBikeToCollection()
        {
            int bikeId = 1;

            IBikeService service =
                new BikeService(context);

            var dbBike = context.Bikes.ToList()
               .Find(b => b.Id == bikeId);

            await service.AddBikeToCollectionAsync(dbBike.Id, user.Id);

            Assert.True(user.UsersBikes.Count() == 1);
        }

        [Test]
        public async Task Test_AddBikeToCollectionThrowsUserException()
        {
            string userId = "4";

            IBikeService service =
                new BikeService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddBikeToCollectionAsync(1, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_AddBikeToCollectionThrowsBikeException()
        {
            string userId = "1";


            IBikeService service =
                new BikeService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddBikeToCollectionAsync(5, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid BikeID"));
        }

        [Test]
        public async Task Test_RemoveBikeToCollection()
        {
            int bikeId = 1;

            IBikeService service =
               new BikeService(context);

            var dbBike = context.Bikes.ToList()
               .Find(c => c.Id == bikeId);

            await service.AddBikeToCollectionAsync(dbBike.Id, user.Id);

            await service.RemoveBikeFromCollectionAsync(dbBike.Id, user.Id);

            Assert.True(user.UsersBikes.Count() == 0);
        }

        [Test]
        public async Task Test_RemoveBikeToCollectionThrowsUserException()
        {
            int bikeId = 1;

            IBikeService service =
               new BikeService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                  async () => await service.RemoveBikeFromCollectionAsync(1,"2"));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_GetRented()
        {
            int bikeId = 1;

            IBikeService service =
               new BikeService(context);

            var dbBike = context.Bikes.ToList()
               .Find(c => c.Id == bikeId);

            await service.AddBikeToCollectionAsync(dbBike.Id, user.Id);

            var userRents = await service.GetRentedAsync(user.Id);

            Assert.True(userRents.Count() == 1);

        }

        [Test]
        public async Task Test_GetRentedThrowsUserException()
        {
           

            IBikeService service =
               new BikeService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.GetRentedAsync("2"));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }


        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
