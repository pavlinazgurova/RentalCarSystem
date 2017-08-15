namespace RentalCar.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class HomeIndexCarModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public bool IsRented { get; set; }
    }
}