using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class BusTrackResponseDto
    {
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }
        [DataMember(Name = "BusId")]
        public Guid BusId { get; set; }
        [DataMember(Name = "LastDestination")]
        public PlaceResponse LastDestination { get; set; }
        [DataMember(Name = "Route")]
        public RouteResponse Route { get; set; }
        [DataMember(Name = "CurrentLattitude")]
        public double CurrentLattitude { get; set; }
        [DataMember(Name = "CurrentLongitude")]
        public double CurrentLongitude { get; set; }
        [DataMember(Name = "StartLattitude")]
        public double StartLattitude { get; set; }
        [DataMember(Name = "StartLongitude")]
        public double StartLongitude { get; set; }
        [DataMember(Name = "GDirection")]
        public string GDirection { get; set; }
        [DataMember(Name = "CurrentRouteStatus")]
        public IEnumerable<PlaceWithETAResponse> CurrentRouteStatus { get; set; }
    }
}
