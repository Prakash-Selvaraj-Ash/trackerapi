using System;
using System.Runtime.Serialization;

namespace BusTrackerApi.DTOS
{
    [DataContract]
    public class UpdateLastDestinationDto
    {
        [DataMember(Name = "LastDestinationId")]
        public Guid LastDestinationId { get; set; }
        [DataMember(Name = "CurrentLocation")]
        public GeoCoordinateDto CurrentLocation { get; set; }
        [DataMember(Name = "BusId")]
        public Guid BusId { get; set; }
    }
}
