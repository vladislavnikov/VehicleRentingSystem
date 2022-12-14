using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VehicleRentingSystem.Data.Models;

namespace VehicleRentingSystem.Models.Boat
{
    public class AddBoatViewModel
    {
        [Required]
        [MaxLength(25)]
        public string Brand { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.0", "300.0", ConvertValueInInvariantCulture = true)]
        public decimal PricePerHour { get; set; }

        [Required]
        public int Power { get; set; }

        public string ImageUrl { get; set; } = null!;

        public List<UserBoat> UsersBoats { get; set; } = new List<UserBoat>();

    }
}
