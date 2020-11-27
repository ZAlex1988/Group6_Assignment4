using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.DataAccess;
using Assignment4.DBModel;
using Assignment4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment4.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<HomeController> log;
        NationalParksData db;

        public ReservationController(ILogger<HomeController> logger, NationalParksData dbContext)
        {
            log = logger;
            db = dbContext;
        }
        public IActionResult Reservations()
        {
            ReservationsViewModel model = new ReservationsViewModel();
            model.reserveRequest = null;
            model.Reservations = null;
            log.LogInformation("In reservation controlller Reservations method");
            return View("/Views/Home/Reservations.cshtml", model);
        }

        public IActionResult Reserve(string ids)
        {

            log.LogInformation($"In Reserve method... val: {ids}");
            ReserveParams reserveParams = new ReserveParams();
            reserveParams.parkId = ids.Split("___")[0];
            reserveParams.campId = ids.Split("___")[1];
            reserveParams.parkName = db.Park.Where(park => park.ParkCode.Equals(reserveParams.parkId)).Select(park => park.ParkName).FirstOrDefault();
            reserveParams.campName = db.Campground.Where(camp => camp.CampgroundId.Equals(reserveParams.campId)).Select(camp=> camp.CampgroundName).FirstOrDefault();
            ReservationsViewModel model = new ReservationsViewModel();
            model.reserveRequest = reserveParams;

            


            return View("/Views/Home/Reservations.cshtml", model);


        }
    }
}
