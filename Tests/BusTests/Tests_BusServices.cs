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

        [SetUp]
        public void TestInitialize()
        {
            this.busList = new List<Bus>()
            {
            new Bus(){Id=1,Brand="Bus1",Power = 150,PricePerHour = 50,Seats = 50 ,ImageUrl = "bus1.jpg"},
            new Bus(){Id=2,Brand="Bus2",Power = 170,PricePerHour = 60, Seats = 60 ,ImageUrl = "bus2.jpg"},
            new Bus(){Id=3,Brand="Bus3",Power = 100,PricePerHour = 40, Seats = 70 ,ImageUrl = "bus3.jpg"}
            };

            var options = new DbContextOptionsBuilder<VehicleDbContext>()
                   .UseInMemoryDatabase(databaseName: "VehiclesInMemoryDb")
                   .Options;
            this.context = new VehicleDbContext(options);
            this.context.AddRangeAsync(this.busList);
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
    }
}
