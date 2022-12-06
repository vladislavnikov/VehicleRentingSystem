using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VehicleRentingSystem.Data.Models;

namespace VehicleRentingSystem.Models.Bus
{
    public class BusViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;
       
        public decimal PricePerHour { get; set; }
       
        public int Seats { get; set; }
        
        public int Power { get; set; }
       
        public string ImageUrl { get; set; }

    }
}
