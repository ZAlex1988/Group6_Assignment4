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
            model.showSearch = true;
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
            model.showSearch = false;
            return View("/Views/Home/Reservations.cshtml", model);

        }

        [HttpPost]
        public IActionResult Thankyou(string CampgroundId, string FirstName, string LastName, string Address1, string Address2, string City, string State, string Zip, string Country, string HomePhone, string WorkPhone, string CellPhone, string Email, string TypeOfCamper,
                        int LengthOfCamper, int Adults, int Children, int Pets, string PetDescription, int Sites, string TypeOfSite, string FullHookup, string Amp50Electric, string PullThrough, string PreferredSite, int FirstNightMonth, int FirstNightDay,
                        int FirstNightYear, string ArrivalTime, int Nights, string Referral, string Comments)
        {
            log.LogInformation($"In Thankyou method... ");
            Reservation res = new Reservation();
            res.FirstName = FirstName;
            res.LastName = LastName;
            res.CampgroundId = CampgroundId;
            res.Address1 = Address1;
            res.Address2 = Address2;
            res.City = City;
            res.State = State;
            res.Zip = Zip;
            res.Country = Country;
            res.HomePhone = HomePhone;
            res.WorkPhone = WorkPhone;
            res.Cellphone = CellPhone;
            res.Email = Email;
            res.TypeOfCamper = TypeOfCamper;
            res.LengthOfCamper = LengthOfCamper;
            res.Adults = Adults;
            res.Children = Children;
            res.Pets = Pets;
            res.PetDescription = PetDescription;
            res.Sites = Sites;
            res.TypeOfSite = TypeOfSite;
            res.FullHookup = FullHookup != null && "true".Equals(FullHookup);
            res.Amp50Electric = Amp50Electric != null && "true".Equals(Amp50Electric);
            res.PullThrough = PullThrough != null && "true".Equals(PullThrough);
            res.PreferredSite = PreferredSite;
            if (FirstNightYear > 0 && FirstNightMonth > 0 && FirstNightDay > 0)
            {
                res.FirstNight = new DateTime(FirstNightYear, FirstNightMonth, FirstNightDay);
            }
            res.ArrivalTime = ArrivalTime;
            res.Nights = Nights;
            res.Referral = Referral;
            res.Comments = Comments;
            db.Reservation.Add(res);
            db.SaveChanges();

            ThankYouPageModel model = new ThankYouPageModel();
            model.reservationId = res.ReservationId.ToString();
            model.type = "Reserve";

            return View("/Views/Home/Thankyou.cshtml", model);

        }

        public IActionResult Find(string resId)
        {
            log.LogInformation($"In reservation controlller Find ({resId}) method");
            ReservationsViewModel model = new ReservationsViewModel();
            model.reserveRequest = null;
            model.Reservations = db.Reservation.Where(res => res.ReservationId.ToString().Equals(resId)).FirstOrDefault();
            model.reservationId = resId;
            model.showSearch = true;
            if (model.Reservations == null)
            {
                model.noResults = true;
            } else {
                model.reserveRequest = new ReserveParams();
                model.reserveRequest.campId = model.Reservations.CampgroundId;
                Campground camp = db.Campground.Where(camp => camp.CampgroundId.Equals(model.Reservations.CampgroundId)).FirstOrDefault();
                model.reserveRequest.parkId = camp.ParkCode;
                model.reserveRequest.campName = camp.CampgroundName;
                model.reserveRequest.parkName = db.Park.Where(park => park.ParkCode.Equals(camp.ParkCode)).Select(park => park.ParkName).FirstOrDefault();
            }
            return View("/Views/Home/Reservations.cshtml", model);
        }

        [HttpPost]
        public IActionResult Update(string CampgroundId, string ReservationId, string FirstName, string LastName, string Address1, string Address2, string City, string State, string Zip, string Country, string HomePhone, string WorkPhone, string CellPhone, string Email, string TypeOfCamper,
                        int LengthOfCamper, int Adults, int Children, int Pets, string PetDescription, int Sites, string TypeOfSite, string FullHookup, string Amp50Electric, string PullThrough, string PreferredSite, int FirstNightMonth, int FirstNightDay,
                        int FirstNightYear, string ArrivalTime, int Nights, string Referral, string Comments)
        {
            log.LogInformation($"In Update method... ");
            Reservation res = db.Reservation.Where(res => res.ReservationId.ToString().Equals(ReservationId)).FirstOrDefault();
            res.FirstName = FirstName;
            res.LastName = LastName;
            res.CampgroundId = CampgroundId;
            res.Address1 = Address1;
            res.Address2 = Address2;
            res.City = City;
            res.State = State;
            res.Zip = Zip;
            res.Country = Country;
            res.HomePhone = HomePhone;
            res.WorkPhone = WorkPhone;
            res.Cellphone = CellPhone;
            res.Email = Email;
            res.TypeOfCamper = TypeOfCamper;
            res.LengthOfCamper = LengthOfCamper;
            res.Adults = Adults;
            res.Children = Children;
            res.Pets = Pets;
            res.PetDescription = PetDescription;
            res.Sites = Sites;
            res.TypeOfSite = TypeOfSite;
            res.FullHookup = FullHookup != null && "true".Equals(FullHookup);
            res.Amp50Electric = Amp50Electric != null && "true".Equals(Amp50Electric);
            res.PullThrough = PullThrough != null && "true".Equals(PullThrough);
            res.PreferredSite = PreferredSite;
            if (FirstNightYear > 0 && FirstNightMonth > 0 && FirstNightDay > 0)
            {
                res.FirstNight = new DateTime(FirstNightYear, FirstNightMonth, FirstNightDay);
            }
            res.ArrivalTime = ArrivalTime;
            res.Nights = Nights;
            res.Referral = Referral;
            res.Comments = Comments;

            db.Reservation.Update(res);
            db.SaveChanges();

            ThankYouPageModel model = new ThankYouPageModel();
            model.reservationId = res.ReservationId.ToString();
            model.type = "Update";

            return View("/Views/Home/Thankyou.cshtml", model);

        }

        [HttpPost]
        public IActionResult Delete(string CampgroundId, string ReservationId, string FirstName, string LastName, string Address1, string Address2, string City, string State, string Zip, string Country, string HomePhone, string WorkPhone, string CellPhone, string Email, string TypeOfCamper,
                        int LengthOfCamper, int Adults, int Children, int Pets, string PetDescription, int Sites, string TypeOfSite, string FullHookup, string Amp50Electric, string PullThrough, string PreferredSite, int FirstNightMonth, int FirstNightDay,
                        int FirstNightYear, string ArrivalTime, int Nights, string Referral, string Comments)
        {
            log.LogInformation($"In Delete method... {ReservationId}");
            Reservation res = db.Reservation.Where(res => res.ReservationId.ToString().Equals(ReservationId)).FirstOrDefault();          

            db.Reservation.Remove(res);
            db.SaveChanges();

            ThankYouPageModel model = new ThankYouPageModel();
            model.reservationId = ReservationId;
            model.type = "Delete";

            return View("/Views/Home/Thankyou.cshtml", model);

        }
    }
}
