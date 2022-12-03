using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class Bike
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Brand { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        [Precision(18, 2)]
        public decimal PricePerHour { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<UserBike> UsersBikes { get; set; } = new List<UserBike>();
    }
}
