using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DbConnector
{
    public class BusTrackContext : DbContext
    {
        public BusTrackContext(DbContextOptions<BusTrackContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<BusTracker> BusTrackers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<LiveTracker> LiveTrackers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<RouteAssociation> RouteAssociations { get; set; }
    }
}
