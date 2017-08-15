using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models.Cars
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(1990, 2050)]
        public int Year { get; set; }

        public string Color { get; set; }

        public double Engine { get; set; }

        public decimal PricePerDay { get; set; }

        public string OwnerId { get; set; }
    }
}