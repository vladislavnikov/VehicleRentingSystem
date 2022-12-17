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
        private User user;
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
            this.context.AddRangeAsync(this.carList);
            this.context.AddRangeAsync(this.carTypeList);
            this.context.AddRangeAsync(this.user);
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

        [Test]
        public async Task Test_AddCarToCollection()
        {
            int carId = 1;

            ICarService service =
               new CarService(context);

            var dbCar = context.Cars.ToList()
               .Find(c => c.Id == carId);

            await service.AddCarToCollectionAsync(dbCar.Id, user.Id);

            Assert.True(user.UsersCars.Count() == 1);

        }

        [Test]
        public async Task Test_AddCarToCollectionThrowsUserException()
        {
            string userId = "4";

            ICarService service =
              new CarService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddCarToCollectionAsync(1, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }

        [Test]
        public async Task Test_AddCarToCollectionThrowsCarException()
        {
            string userId = "1";

            ICarService service =
               new CarService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                 async () => await service.AddCarToCollectionAsync(5, userId));

            Assert.That(ex.Message, Is.EqualTo("Invalid CarID"));
        }

        [Test]
        public async Task Test_RemoveCarToCollection()
        {
            int carId = 1;

            ICarService service =
               new CarService(context);

            var dbCar = context.Cars.ToList()
               .Find(c => c.Id == carId);

            await service.AddCarToCollectionAsync(dbCar.Id, user.Id);

            await service.RemoveCarFromCollectionAsync(dbCar.Id, user.Id);

            Assert.True(user.UsersCars.Count() == 0);

        }

        [Test]
        public async Task Test_RemoveCarToCollectionThrowsUserException()
        {
            ICarService service =
              new CarService(context);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                  async () => await service.RemoveCarFromCollectionAsync(1, "2"));

            Assert.That(ex.Message, Is.EqualTo("Invalid UserID"));
        }


        [Test]
        public async Task Test_GetRented()
        {
            int carId = 1;

            ICarService service =
               new CarService(context);

            var dbCar = context.Cars.ToList()
               .Find(c => c.Id == carId);

            await service.AddCarToCollectionAsync(dbCar.Id, user.Id);

            var userRents = await service.GetRentedAsync(user.Id);

            Assert.True(userRents.Count() == 1);

        }

        [Test]
        public async Task Test_GetRentedThrowsUserException()
        {
            ICarService service =
                new CarService(context);

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