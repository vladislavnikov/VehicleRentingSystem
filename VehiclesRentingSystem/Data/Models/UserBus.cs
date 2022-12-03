using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class UserBus
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int BusId { get; set; }

        [ForeignKey(nameof(BusId))]
        public Bus Bus { get; set; }
    }
}
