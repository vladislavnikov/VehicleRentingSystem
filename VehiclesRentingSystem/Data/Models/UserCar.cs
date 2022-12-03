using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class UserCar
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }
    }
}
