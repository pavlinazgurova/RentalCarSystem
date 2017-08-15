namespace RentalCar.Migrations
{
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<RentalCar.Data.CarsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "RentalCar.Data.CarsDbContext";
        }

        protected override void Seed(RentalCar.Data.CarsDbContext context)
        {
            if (context.Cars.Any())
            {
                return;
            }

            var user = context.Users.FirstOrDefault();

            if (user == null)
            {
                return;
            }

            context.Cars.Add(new Car
            {
                Make = "BMW",
                Model = "650i",
                Color = "Black",
                Engine = 5.0,
                EngineType = EngineType.Gasoline,
                ImageUrl = "https://s-media-cache-ak0.pinimg.com/736x/5e/d2/c7/5ed2c7f89aa87f3c5f8a737114f72207--bmw-convertible-bmw-i.jpg",
                Power = 5000,
                PricePerDay = 5000,
                Year = 2016,
                OwnerId = user.Id
            });

            context.Cars.Add(new Car
            {
                Make = "BMW",
                Model = "640i",
                Color = "Orange",
                Engine = 5.0,
                EngineType = EngineType.Gasoline,
                ImageUrl = "https://s-media-cache-ak0.pinimg.com/736x/5e/d2/c7/5ed2c7f89aa87f3c5f8a737114f72207--bmw-convertible-bmw-i.jpg",
                Power = 200,
                PricePerDay = 2300,
                Year = 2016,
                OwnerId = user.Id
            });

            context.Cars.Add(new Car
            {
                Make = "BMW",
                Model = "660i",
                Color = "White",
                Engine = 5.0,
                EngineType = EngineType.Gasoline,
                ImageUrl = "https://s-media-cache-ak0.pinimg.com/736x/5e/d2/c7/5ed2c7f89aa87f3c5f8a737114f72207--bmw-convertible-bmw-i.jpg",
                Power = 600,
                PricePerDay = 360,
                Year = 2017,
                OwnerId = user.Id
            });

            context.Cars.Add(new Car
            {
                Make = "Mercedes",
                Model = "c200",
                Color = "White",
                Engine = 7.0,
                EngineType = EngineType.Diesel,
                ImageUrl = "http://img.automobile.de/modellbilder/Mercedes-Benz-C-200-24870_1248687633703.jpg",
                Power = 700,
                PricePerDay = 700,
                Year = 2017,
                OwnerId = user.Id
            });

            context.Cars.Add(new Car
            {
                Make = "Mercedes",
                Model = "c200",
                Color = "Grey",
                Engine = 2.0,
                EngineType = EngineType.Other,
                ImageUrl = "http://img.automobile.de/modellbilder/Mercedes-Benz-C-200-24870_1248687633703.jpg",
                Power = 300,
                PricePerDay = 500,
                Year = 2012,
                OwnerId = user.Id
            });

            context.Cars.Add(new Car
            {
                Make = "Mercedes",
                Model = "s550",
                Color = "Blue",
                Engine = 4.0,
                EngineType = EngineType.Diesel,
                ImageUrl = "http://img.automobile.de/modellbilder/Mercedes-Benz-C-200-24870_1248687633703.jpg",
                Power = 250,
                PricePerDay = 900,
                Year = 2015,
                OwnerId = user.Id
            });

            context.Cars.Add(new Car
            {
                Make = "Mercedes",
                Model = "s550",
                Color = "Black",
                Engine = 2.0,
                EngineType = EngineType.Diesel,
                ImageUrl = "http://img.automobile.de/modellbilder/Mercedes-Benz-C-200-24870_1248687633703.jpg",
                Power = 450,
                PricePerDay = 900,
                Year = 2014,
                OwnerId = user.Id
            });
        }
    }
}
