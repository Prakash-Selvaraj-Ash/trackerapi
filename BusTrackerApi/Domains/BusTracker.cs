using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTrackerApi.Domains
{
    public class BusTracker : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }
        public Guid BusId { get; set; }
        [ForeignKey("LastDestinationId")]
        public virtual Place LastDestination { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        public Guid RouteId { get; set; }
        public Guid? LastDestinationId { get; set; }
        public double CurrentLattitude { get; set; }
        public double CurrentLongitude { get; set; }
        public double StartLattitude { get; set; }
        public double StartLongitude { get; set; }
        public string GDirection { get; set; }
        public string CurrentRouteStatus { get; set; }
    }
}
