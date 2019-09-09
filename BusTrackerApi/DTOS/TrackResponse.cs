using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class TrackResponse
    {
        [DataMember(Name = "LastDestination")]
        public PlaceResponse LastDestination { get; set; }
        [DataMember(Name = "CurrentLocationCoordinate")]
        public GeoCoordinateDto CurrentLocationCoordinate { get; set; }
        [DataMember(Name = "Places")]
        public IEnumerable<PlaceWithETAResponse> Places { get; set; }
    }
}
