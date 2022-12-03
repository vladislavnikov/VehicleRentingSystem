using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VehicleRentingSystem.Data.Models;

namespace VehicleRentingSystem.Models.Car
{
    public class AddCarViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Brand { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.0", "300.0", ConvertValueInInvariantCulture = true)]
        public decimal PricePerHour { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public int Power { get; set; }

        public string ImageUrl { get; set; } = null!;

        public int CarTypeId { get; set; }

        public List<CarType> CarTypes { get; set; } = new List<CarType>();
    }
}

