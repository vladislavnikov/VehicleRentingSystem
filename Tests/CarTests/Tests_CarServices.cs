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
        private IEnumerable<Car> carList;
        private IEnumerable<CarType> carTypeList;
        private VehicleDbContext context;

        [SetUp]
        public void TestInitialize()
        {
            this.carList = new List<Car>()
            {
            new Car(){Id=1,Brand="Audi",Power = 150,PricePerHour = 50, ImageUrl = "caraudi.com", CarTypeId=1},
            new Car(){Id=2,Brand="BMW",Power = 170,PricePerHour = 60, ImageUrl = "carbmw.com", CarTypeId=1},
            new Car(){Id=3,Brand="Honda",Power = 100,PricePerHour = 40, ImageUrl = "carhonda.com", CarTypeId=1}
            };

            this.carTypeList = new List<CarType>()
            { 
            new CarType(){Id = 1, Name = "Sedan"},
            new CarType(){Id = 2, Name = "Coupe"}
            };


            var options = new DbContextOptionsBuilder<VehicleDbContext>()
                   .UseInMemoryDatabase(databaseName: "VehiclesInMemoryDb")
                   .Options;
            this.context = new VehicleDbContext(options);
            this.context.AddRangeAsync(this.carList);
            this.context.AddRangeAsync(this.carTypeList);
            this.context.SaveChangesAsync();

        }

        [Test]
        public async Task Test_DeleteCars()
        {
            int carId = 1;

            ICarService service =
                new CarService(context);

            await service.DeleteCarAsync(carId);
            await context.SaveChangesAsync();

            var dbCar = context.Cars.ToList()
                .Find(c => c.Id == carId);

            Assert.True(dbCar == null);
        }

        [Test]
        public async Task Test_AddCar()
        {
            AddCarViewModel car = new AddCarViewModel()
            {
                Brand = "Ford",
                Power = 140,
                PricePerHour = 45,
                ImageUrl = "ford.jpg",
                CarTypeId = 1
            };

            ICarService service =
                new CarService(context);

            await service.AddCarAsync(car);
            await context.SaveChangesAsync();

            var dbCars = context.Cars.ToList();

            Assert.True(dbCars.Count() == 4);
        }

        [Test]
        public async Task Test_GetAllCars()
        {
            ICarService service =
                new CarService(context);

            var dbCars = await service.GetAllCarAsync();

            Assert.True(dbCars.Count() == 3);
        }

        [Test]
        public async Task Test_GetAllTypes()
        {
            ICarService service =
                new CarService(context);

            var dbTypes = await service.GetCarTypesAsync();

            Assert.True(dbTypes.Count() == 2);
            
        }
    }
}