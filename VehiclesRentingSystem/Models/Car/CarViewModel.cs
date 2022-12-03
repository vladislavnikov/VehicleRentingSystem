namespace VehicleRentingSystem.Models.Car
{
    public class CarViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public decimal PricePerHour { get; set; } 

        public string? CarType { get; set; }

        public int Power { get; set; }

        public string ImageUrl { get; set; } = null!;
    } 
}
