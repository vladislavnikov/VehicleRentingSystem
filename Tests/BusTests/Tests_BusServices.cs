using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Boat;
using VehicleRentingSystem.Models.Bus;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Tests.BusTests
{
    [TestFixture]
    public  class Tests_BusServices
    {
        private IEnumerable<Bus> busList;
        private VehicleDbContext context;
        private User user;

        [SetUp]
        public void TestInitialize()
        {
            this.busList = new List<Bus>()
            {
            new Bus(){Id=1,Brand="Bus1",Power = 150,PricePerHour = 50,Seats = 50 ,ImageUrl = "bus1.jpg"},
            new Bus(){Id=2,Brand="Bus2",Power = 170,PricePerHour = 60, Seats = 60 ,ImageUrl = "bus2.jpg"},
            new Bus(){Id=3,Brand="Bus3",Power = 100,PricePerHour = 40, Seats = 70 ,ImageUrl = "bus3.jpg"}
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
            this.context.AddRangeAsync(this.busList);
            this.context.AddRangeAsync(this.user);
            this.context.SaveChangesAsync();
        }

        [Test]
        public async Task Test_DeleteBus()
        {
            int busId = 1;

            IBusService service =
                new BusService(context);

            await service.DeleteBusAsync(busId);
            await context.SaveChangesAsync();

            var dbBus = context.Buses.ToList()
                .Find(b => b.Id == busId);

            Assert.True(dbBus == null);
        }

        [Test]
        public async Task Test_AddBus()
        {
            AddBusViewModel car = new AddBusViewModel()
            {
                Brand = "Bus4",
                Power = 140,
                PricePerHour = 45,
                Seats = 60 ,
                ImageUrl = "bus4.jpg",
            };

            IBusService service =
                new BusService(context);

            await service.AddBusAsync(car);
            await context.SaveChangesAsync();

            var dbBuses = context.Buses.ToList();

            Assert.True(dbBuses.Count() == 4);
        }

        [Test]
        public async Task Test_GetAllBuses()
        {
            IBusService service =
                new BusService(context);

            var dbBuses = await service.GetAllBusAsync();

            Assert.True(dbBuses.Count() == 3);
        }

        [Test]
        public async Task Test_AddBusToCollection()
        {
            int BusId = 1;

            IBusService service =
                new BusService(context);

            var dbBus = context.Buses.ToList()
               .Find(b => b.Id == BusId);

            await service.AddBusToCollectionAsync(dbBus.Id, user.Id);

            Assert.True(user.UsersBuses.Count() == 1);
        }

        [Test]
        public async Task Test_RemoveBusToCollection()
        {
            int busId = 1;

            IBusService service =
                new BusService(context);

            var dbBus = context.Buses.ToList()
               .Find(b => b.Id == busId);

            await service.AddBusToCollectionAsync(dbBus.Id, user.Id);

            await service.RemoveBusFromCollectionAsync(dbBus.Id, user.Id);

            Assert.True(user.UsersBuses.Count() == 0);
        }

        [Test]
        public async Task Test_RemoveBusToCollectionThrowsUserException()
        {
            IBusService service =
              new BusService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                  async () => await service.RemoveBusFromCollectionAsync(1, "2"));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_GetRented()
        {
            int busId = 1;

            IBusService service =
               new BusService(context);

            var dbBoat = context.Buses.ToList()
               .Find(c => c.Id == busId);

            await service.AddBusToCollectionAsync(dbBoat.Id, user.Id);

            var userRents = await service.GetRentedAsync(user.Id);

            Assert.True(userRents.Count() == 1);

        }

        [Test]
        public async Task Test_GetRentedThrowsUserException()
        {
            IBusService service =
              new BusService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.GetRentedAsync("2"));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_AddBusToCollectionThrowsUserException()
        {
            string userId = "4";

            IBusService service =
                new BusService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddBusToCollectionAsync(1, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_AddBusToCollectionThrowsBusException()
        {
            string userId = "1";


            IBusService service =
                new BusService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddBusToCollectionAsync(5, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid BusID"));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
