using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.ComponentModel.DataAnnotations;
using VehicleRentingSystem.Data.Models;
using VehiclesRentingSystem.Data;
using NUnit.Framework;
using System;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Services;
using VehicleRentingSystem.Models.Car;

namespace VehicleRentingSystem.Tests.CarTests
{
    [TestFixture]
    public class Tests
    {
        private IEnumerable<CarViewModel> cars;
        private VehicleDbContext context;

        [SetUp]
        public void TestInitialize()
        {
            this.cars = new List<CarViewModel>()
            { 
            new CarViewModel(){Id=1,Brand="Audi",Power = 150,PricePerHour = 50, ImageUrl = "caraudi.com"},
            new CarViewModel(){Id=2,Brand="BMW",Power = 170,PricePerHour = 60, ImageUrl = "carbmw.com"},
            new CarViewModel(){Id=2,Brand="Honda",Power = 100,PricePerHour = 40, ImageUrl = "carhonda.com"}
            };

            var options = new DbContextOptionsBuilder<VehicleDbContext>()
                   .UseInMemoryDatabase(databaseName: "VehiclesInMemoryDb") // Give a Unique name to the DB
                   .Options;
            context= new VehicleDbContext(options);
            context.AddRange(cars);
            context.SaveChanges();

        }

        [Test]
        public void Test_DeleteCars()
        {
            int carId = 1;

            ICarService service =
                new CarService(context);

            //Delete from context
            service.DeleteCarAsync(carId);
            context.SaveChanges();

            var dbCars = context.Cars.Select(c => new Car()
            {
                Id = c.Id,
                Brand = c.Brand,
                Power = c.Power,
                PricePerHour = c.PricePerHour,
                ImageUrl = c.ImageUrl
            }).ToList();

            Assert.True(dbCars.Count() == 2);

        }
    }
}