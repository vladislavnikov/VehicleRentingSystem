using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Models.Trucks
{
    public class TruckViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public decimal PricePerHour { get; set; }

        public int Power { get; set; }

        public double MaxWeight { get; set; }

        public string ImageUrl { get; set; }
    }
}
