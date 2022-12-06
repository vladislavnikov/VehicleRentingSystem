using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VehicleRentingSystem.Data.Models;

namespace VehicleRentingSystem.Models.Bike
{
    public class BikeViewModel
    {
        public int Id { get; set; }
      
        public string Brand { get; set; } = null!;
     
        public decimal PricePerHour { get; set; }
       
        public string ImageUrl { get; set; }
       
    }
}
