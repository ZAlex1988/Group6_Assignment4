using Assignment4.DBModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4.DataAccess
{
    public class NationalParksData : DbContext
    {
        public NationalParksData(DbContextOptions<NationalParksData> options) : base(options) { }

        public DbSet<Park> Park { get; set; }

        public DbSet<Activity> Activity { get; set; }

        public DbSet<ParkActivity> ParkActivity { get; set; }

        public DbSet<Fee> Fee { get; set; }

        public DbSet<Campground> Campground { get; set; }

        public DbSet<Reservation> Reservation { get; set; }

        public DbSet<ParkState> ParkState { get; set; }
        public DbSet<ParkImages> ParkImages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkActivity>().HasKey(vf => new { vf.ParkCode, vf.ActivityId });
            modelBuilder.Entity<ParkState>().HasKey(vf => new { vf.ParkCode, vf.StateCode });
        }

    }


}
