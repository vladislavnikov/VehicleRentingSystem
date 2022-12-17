using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Boat;
using VehicleRentingSystem.Models.Car;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Tests.BoatTest
{
    [TestFixture]
    public class Tests_BoatServices
    {
        private IEnumerable<Boat> boatList;
        private VehicleDbContext context;
        private User user;

        [SetUp]
        public void TestInitialize()
        {
            this.boatList = new List<Boat>()
            {
            new Boat(){Id=1,Brand="Yamaha",Power = 150,PricePerHour = 50, ImageUrl = "yamaha.com"},
            new Boat(){Id=2,Brand="Toyota",Power = 170,PricePerHour = 60, ImageUrl = "toyota.com"},
            new Boat(){Id=3,Brand="Suzuki",Power = 100,PricePerHour = 40, ImageUrl = "suzuki.com"}
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
            this.context.AddRangeAsync(this.boatList);
            this.context.AddRangeAsync(this.user);
            this.context.SaveChangesAsync();
        }

        [Test]
        public async Task Test_DeleteBoats()
        {
            int boatId = 1;

            IBoatService service =
                new BoatService(context);

            await service.DeleteBoatAsync(boatId);
            await context.SaveChangesAsync();

            var dbBoat = context.Boats.ToList()
                .Find(b => b.Id == boatId);

            Assert.True(dbBoat == null);
        }

        [Test]
        public async Task Test_AddBoat()
        {
            AddBoatViewModel car = new AddBoatViewModel()
            {
                Brand = "Suzuki",
                Power = 140,
                PricePerHour = 45,
                ImageUrl = "ford.jpg",
            };

            IBoatService service =
                new BoatService(context);

            await service.AddBoatAsync(car);
            await context.SaveChangesAsync();

            var dbBoats = context.Boats.ToList();

            Assert.True(dbBoats.Count() == 4);
        }

        [Test]
        public async Task Test_AddBoatToCollection()
        {
            int boatId = 1;

            IBoatService service =
                new BoatService(context);

            var dbBoat = context.Boats.ToList()
               .Find(b => b.Id == boatId);

            await service.AddBoatToCollectionAsync(dbBoat.Id, user.Id);

            Assert.True(user.UsersBoats.Count() == 1);
        }

        [Test]
        public async Task Test_AddBoatToCollectionThrowsUserException()
        {
            string userId = "4";


            IBoatService service =
               new BoatService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddBoatToCollectionAsync(1, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_AddBoatToCollectionThrowsBoatException()
        {
            string userId = "1";


            IBoatService service =
                new BoatService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddBoatToCollectionAsync(5, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid BoatID"));
        }

        [Test]
        public async Task Test_RemoveBoatToCollection()
        {
            int boatId = 1;

            IBoatService service =
               new BoatService(context);

            var dbBoat = context.Boats.ToList()
               .Find(c => c.Id == boatId);

            await service.AddBoatToCollectionAsync(dbBoat.Id, user.Id);

            await service.RemoveBoatFromCollectionAsync(dbBoat.Id, user.Id);

            Assert.True(user.UsersBoats.Count() == 0);
        }

        [Test]
        public async Task Test_RemoveBoatToCollectionThrowsUserException()
        {
            IBoatService service =
              new BoatService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                  async () => await service.RemoveBoatFromCollectionAsync(1, "2"));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_GetAllBoats()
        {
            IBoatService service =
                new BoatService(context);

            var dbBoats = await service.GetAllBoatAsync();

            Assert.True(dbBoats.Count() == 3);
        }

        [Test]
        public async Task Test_GetRented()
        {
            int boatId = 1;

            IBoatService service =
               new BoatService(context);

            var dbBoat = context.Boats.ToList()
               .Find(c => c.Id == boatId);

            await service.AddBoatToCollectionAsync(dbBoat.Id, user.Id);

            var userRents = await service.GetRentedAsync(user.Id);

            Assert.True(userRents.Count() == 1);

        }

        [Test]
        public async Task Test_GetRentedThrowsUserException()
        {
            IBoatService service =
               new BoatService(context);

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
