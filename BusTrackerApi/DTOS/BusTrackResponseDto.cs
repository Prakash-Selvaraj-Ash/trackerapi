using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class BusTrackResponseDto
    {
        public Guid Id { get; set; }
        public Guid BusId { get; set; }
        public PlaceResponse LastDestination { get; set; }
        public RouteResponse Route { get; set; }
        public double CurrentLattitude { get; set; }
        public double CurrentLongitude { get; set; }
        public double StartLattitude { get; set; }
        public double StartLongitude { get; set; }
        public string GDirection { get; set; }
        public IEnumerable<PlaceWithETAResponse> CurrentRouteStatus { get; set; }
    }
}
