using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.DBModel
{
    public class Activity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public List<ParkActivity> ParkActivity;
    }

    public class Park
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string ParkDescription { get; set; }

        public string ParkUrl { get; set; }

        public List<ParkActivity> ParkActivity;

        public List<Fee> Fee;

        public List<Campground> Campground;

        public List<ParkState> ParkState;

        public List<ParkImages> ParkImages;
    }

    public class ParkActivity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Park")]
        public string ParkCode { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Activity")]
        public string ActivityId { get; set; }
        public virtual Activity activity { get; set; }
        public virtual Park Park { get; set; }
    }

    public class ParkState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Park")]
        public string ParkCode { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StateCode { get; set; }

        public virtual Park Park { get; set; }
    }

    public class ParkImages
    {
        [Key]
        public int ImageId { get; set; }

        [ForeignKey("Park"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ParkCode { get; set; }

        public string ImageUrl { get; set; }
        public string Title { get; set; }

        public string Caption { get; set; }

        public virtual Park Park { get; set; }
    }

    public class Fee
    {
        [Key]
        public int FeeId { get; set; }

        [ForeignKey("Park"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ParkCode { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public virtual Park Park { get; set; }

    }

    public class Campground
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CampgroundId { get; set; }
        public string CampgroundName { get; set; }
        public string CampgroundDescription { get; set; }
        public int MaxReservation { get; set; }
        public int Sites { get; set; }
        public Boolean Reservable { get; set; }

        [ForeignKey("Park"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ParkCode { get; set; }

        public List<Reservation> Reservations;
        public virtual Park Park { get; set; }

    }

    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ReservationId { get; set; }

        [ForeignKey("Campground"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CampgroundId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string TypeOfCamper { get; set; }
        public int LengthOfCamper { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Pets { get; set; }
        public string PetDescription { get; set; }
        public int Sites { get; set; }
        public string TypeOfSite { get; set; }
        public bool Amp50Electric { get; set; }
        public bool FullHookup { get; set; }
        public bool PullThrough { get; set; }
        public string PreferredSite { get; set; }
        public DateTime FirstNight { get; set; }
        public string ArrivalTime { get; set; }
        public int Nights { get; set; }
        public string Referral { get; set; }
        public string Comments { get; set; }
        public virtual Campground campground { get; set; }
    }

}
