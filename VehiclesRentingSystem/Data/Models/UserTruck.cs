using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class UserTruck
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int TruckId { get; set; }

        [ForeignKey(nameof(TruckId))]
        public Truck Truck { get; set; }
    }
}
