namespace RentalCar.Models.Cars
{
    using Data;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CreateCarModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(1990, 2050)]
        public int Year { get; set; }

        public string Color { get; set; }

        public double Engine { get; set; }

        [Display(Name = "Engine Type")]
        [ScaffoldColumn(false)]
        public EngineType EngineType { get; set; }

        public int? Power { get; set; }

        [Display(Name = "Price in BGN Per Day")]
        public decimal PricePerDay { get; set; }

        [Required]
        [Url]
        [ImageUrl]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}