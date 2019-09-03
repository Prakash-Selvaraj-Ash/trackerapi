using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    public class TrackResponse
    {
        public string LastDestination { get; set; }
        public GeoCoordinateDto LastDestinationCoordinate { get; set; }
        public GeoCoordinateDto CurrentLocationCoordinate { get; set; }
    }
}
