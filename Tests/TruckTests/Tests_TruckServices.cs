﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Models.Boat;
using VehicleRentingSystem.Models.Trucks;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

namespace VehicleRentingSystem.Tests.TruckTests
{
    [TestFixture]
    public class Tests_TruckServices
    {
        private IEnumerable<Truck> truckList;
        private VehicleDbContext context;

        [SetUp]
        public void TestInitialize()
        {
            this.truckList = new List<Truck>()
            {
            new Truck(){Id=1,Brand="Scania",Power = 150,PricePerHour = 50,MaxWeight = 1000 ,ImageUrl = "scania.com"},
            new Truck(){Id=2,Brand="Man",Power = 170,PricePerHour = 60,MaxWeight = 1000 , ImageUrl = "man.com"},
            new Truck(){Id=3,Brand="Daff",Power = 100,PricePerHour = 40,MaxWeight = 1000 , ImageUrl = "daff.com"}
            };

            var options = new DbContextOptionsBuilder<VehicleDbContext>()
                   .UseInMemoryDatabase(databaseName: "VehiclesInMemoryDb")
                   .Options;
            this.context = new VehicleDbContext(options);
            this.context.AddRangeAsync(this.truckList);
            this.context.SaveChangesAsync();
        }

        [Test]
        public async Task Test_DeleteTrucks()
        {
            int truckId = 1;

            ITruckService service =
                new TruckService(context);

            await service.DeleteTruckAsync(truckId);
            await context.SaveChangesAsync();

            var dbTruck = context.Trucks.ToList()
                .Find(b => b.Id == truckId);

            Assert.True(dbTruck == null);
        }

        [Test]
        public async Task Test_AddTruck()
        {
            AddTruckViewModel car = new AddTruckViewModel()
            {
                Brand = "Suzuki",
                Power = 140,
                PricePerHour = 45,
                MaxWeight = 1000,
                ImageUrl = "ford.jpg",
            };

            ITruckService service =
                 new TruckService(context);

            await service.AddTruckAsync(car);
            await context.SaveChangesAsync();

            var dbTrucks = context.Trucks.ToList();

            Assert.True(dbTrucks.Count() == 4);
        }

        [Test]
        public async Task Test_GetAllTrucks()
        {
            ITruckService service =
                new TruckService(context);

            var dbTrucks = await service.GetAllTruckAsync();

            Assert.True(dbTrucks.Count() == 3);
        }
    }
}
