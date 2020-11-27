using Assignment4.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.Models
{
    public class ReservationsViewModel
    {
        public ReserveParams reserveRequest { get; set; }
        public Reservation Reservations { get; set; }
        public Boolean noResults { get; set; }
    }
}
