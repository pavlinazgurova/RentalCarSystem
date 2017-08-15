namespace RentalCar.Controllers
{
    using Data;
    using Microsoft.AspNet.Identity;
    using Models.Cars;
    using Models.Renting;
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class CarsController : Controller
    {
        public ActionResult MyCars(int page = 1,
            string user = null,
            string search = null)
        {
            var db = new CarsDbContext();

            var pageSize = 5;

            var carsQuery = db.Cars.AsQueryable();

            if (search != null)
            {
                carsQuery = carsQuery
                    .Where(c => c.Make.ToLower().Contains(search.ToLower()) ||
                    c.Model.ToLower().Contains(search.ToLower()));
            }

            if (user != null)
            {
                carsQuery = carsQuery
                    .Where(c => c.Owner.Email == user);
            }

            var cars = carsQuery
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new HomeIndexCarModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    IsRented = c.IsRented
                })
                .ToList();

            ViewBag.CurrentPage = page;

            return View(cars);
        }

        public ActionResult All(int page = 1, 
            string user = null,
            string search = null)
        {
            var db = new CarsDbContext();

            var pageSize = 5;

            var carsQuery = db.Cars.AsQueryable();

            if (search != null)
            {
                carsQuery = carsQuery
                    .Where(c => c.Make.ToLower().Contains(search.ToLower()) ||
                    c.Model.ToLower().Contains(search.ToLower()));
            }

            if (user != null)
            {
                carsQuery = carsQuery
                    .Where(c => c.Owner.Email == user);
            }

            var cars = carsQuery
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new HomeIndexCarModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year,
                    IsRented = c.IsRented
                })
                .ToList();

            ViewBag.CurrentPage = page;

            return View(cars);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateCarModel carModel)
        {
            if (carModel != null && ModelState.IsValid)
            {
                var ownerId = this.User.Identity.GetUserId();

                var car = new Car
                {
                    Make = carModel.Make,
                    Model = carModel.Model,
                    Color = carModel.Color,
                    Engine = carModel.Engine,
                    EngineType = carModel.EngineType,
                    ImageUrl = carModel.ImageUrl,
                    Power = carModel.Power,
                    PricePerDay = carModel.PricePerDay,
                    Year = carModel.Year,
                    OwnerId = ownerId
                };

                var db = new CarsDbContext();

                db.Cars.Add(car);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = car.Id });
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var db = new CarsDbContext();

            var car = db.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsModel
                {
                    Id = c.Id,
                    Color = c.Color,
                    Engine = c.Engine,
                    EngineType = c.EngineType,
                    Make = c.Make,
                    Model = c.Model,
                    Power = c.Power,
                    PricePerDay = c.PricePerDay,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    IsRented = c.IsRented,
                    TotalRents = c.Rentings.Count(),
                    ContactInformation = c.Owner.Email
                })
                .FirstOrDefault();

            if (car == null)
            {
                return HttpNotFound();
            }
           
            return View(car);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Rent(RentCarModel  rentCarModel)
        {
            if (rentCarModel.Days < 1 || rentCarModel.Days > 10)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var db = new CarsDbContext();

            var car = db.Cars
                .Where(c => c.Id == rentCarModel.CarId)
                .Select(c => new {
                    c.IsRented,
                    c.PricePerDay,
                    c.ImageUrl,
                    FullName = c.Make + " " + c.Model + " (" + c.Year + ")"  
                })
                .FirstOrDefault();

            if (car == null || car.IsRented)
            {
                return HttpNotFound();
            }

            rentCarModel.CarName = car.FullName;
            rentCarModel.CarImageUrl = car.ImageUrl;
            rentCarModel.PricePerDay = car.PricePerDay;
            rentCarModel.TotalPrice = car.PricePerDay * rentCarModel.Days;

            return View(rentCarModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Rent(int carId, int days)
        {
            var db = new CarsDbContext();

            var car = db.Cars
                .Where(c => c.Id == carId)
                .FirstOrDefault();

            var userId = this.User.Identity.GetUserId();

            if (car == null || car.IsRented || car.OwnerId == userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var renting = new Renting
            {
                CarId = carId,
                Days = days,
                RentedOn = DateTime.Now,
                UserId = userId,
                TotalPrice = days * car.PricePerDay
            };

            car.IsRented = true;

            db.Rentings.Add(renting);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = car.Id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            using (var db = new CarsDbContext())
            {
                var car = db.Cars.Find(id); 

                var carViewModel = new CarViewModel
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Color = car.Color,
                    Year = car.Year,
                    Engine = car.Engine,
                    PricePerDay = car.PricePerDay,
                    OwnerId = car.OwnerId
                };

                return View(carViewModel);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CarViewModel carModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CarsDbContext())
                {
                    var car = db.Cars.Find(carModel.Id);

                    car.Make = carModel.Make;
                    car.Model = carModel.Model;
                    car.Color = carModel.Color;
                    car.Year = carModel.Year;
                    car.Engine = carModel.Engine;
                    car.PricePerDay = carModel.PricePerDay;

                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = carModel.Id });
            }

            return View(carModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new CarsDbContext())
            {
                Car car = db.Cars.Find(id);

                if (car == null)
                {
                    return HttpNotFound();
                }

                return View(car);
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            using (var db = new CarsDbContext())
            {
                Car car = db.Cars.Find(id);

                if (car == null)
                {
                    return HttpNotFound();
                }

                db.Cars.Remove(car);
                db.SaveChanges();

                return RedirectToAction("All");
            }
        }
    }
}