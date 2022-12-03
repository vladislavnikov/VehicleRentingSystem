using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class Bus
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
        public int Seats { get; set; }

        [Required]
        [MaxLength(1000)]
        public int Power { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<UserBus> UsersBuses { get; set; } = new List<UserBus>();
    }
}
