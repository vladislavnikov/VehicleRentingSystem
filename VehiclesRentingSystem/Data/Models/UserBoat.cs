using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class UserBoat
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int BoatId { get; set; }

        [ForeignKey(nameof(BoatId))]
        public Boat Boat { get; set; }
    }
}
