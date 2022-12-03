using System.ComponentModel.DataAnnotations;

namespace VehicleRentingSystem.Data.Models
{
    public class CarType
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
