using Microsoft.AspNetCore.Identity;

namespace VehicleRentingSystem.Data.Models
{
    public class User : IdentityUser
    {
        public List<UserCar> UsersCars { get; set; } = new List<UserCar>();

        public List<UserBus> UsersBuses { get; set; } = new List<UserBus>();

        public List<UserTruck> UsersTrucks { get; set; } = new List<UserTruck>();

        public List<UserBike> UsersBikes { get; set; } = new List<UserBike>();

        public List<UserBoat> UsersBoats { get; set; } = new List<UserBoat>();
    }
}
