namespace RentalCar.Controllers
{
    using Models.Cars;
    using RentalCar.Data;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var db = new CarsDbContext();

            var cars = db.Cars
                .Where(c => !c.IsRented)
                .OrderByDescending(c => c.Id)
                .Take(3)
                .Select(c => new HomeIndexCarModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Make = c.Make,
                    Model = c.Model,
                    Year = c.Year
                })
                .ToList();

            return View(cars);
        }  
    }
}