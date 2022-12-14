using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VehicleRentingSystem.Data.Models;

namespace VehicleRentingSystem.Models.Trucks
{
    public class AddTruckViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Brand { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.0", "300.0", ConvertValueInInvariantCulture = true)]
        public decimal PricePerHour { get; set; }

        [Required]
        public int Power { get; set; }

        public double MaxWeight { get; set; }

        public string ImageUrl { get; set; }

        public List<UserTruck> UsersTrucks { get; set; } = new List<UserTruck>();

    }
}
