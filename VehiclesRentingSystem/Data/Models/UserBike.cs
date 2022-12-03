using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class UserBike
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int BikeId { get; set; }

        [ForeignKey(nameof(BikeId))]
        public Bike Bike { get; set; }
    }
}
