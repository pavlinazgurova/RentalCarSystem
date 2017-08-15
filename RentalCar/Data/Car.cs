namespace RentalCar.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public Car()
        {
            this.Rentings = new HashSet<Renting>();
        }

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

        public EngineType EngineType { get; set; }

        public int? Power { get; set; }

        public decimal PricePerDay { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsRented { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Renting> Rentings { get; set; }

        internal Car FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}